import scipy.io as sio
import numpy as np
import random

def l2_norm(f):
    f /= (np.linalg.norm(f) + 10e-6)
    return f

class SJE:
    # Structured Joint Embedding based on Akata et al. work
    # x,y data x: embedded input, y: output (labels)
    # D is input embedding dimension (=len(X[0]))
    # E is a vector of output embeddings dimensions (#K)(=len(Y[i][k]))
    # Eta learning rate for each iteration (#T)
    # T iterations number (epochs)
    # counts data instances from each class
    # normalization normalize input projection option
    # classesNum number of different classes
    def __init__(self, inputFileDir, outputEmbs, epochs, etas, classesNum):
        ext = inputFileDir.split('.')[1]
        data = np.load(inputFileDir,encoding='bytes').item()
        self.x = data[b'x']
        y = data[b'y']
        self.counts  = data[b'counts']
        self.images = data[b'images']
        self.testSet = []
        self.testImages=[]
        self.y = self.embedClasses(y,outputEmbs)
        self.D = np.array(self.x).shape[1]
        self.E = np.array(self.y).shape[1]
        self.T= epochs
        self.Eta = etas
        self.classesNum= classesNum
        self.classEmdes = outputEmbs
        self.W = None

    def train(self,X,Y,classEmdes,LR,T):
        # total number of training samples
        N = len(X)
        # initialize the joint embedding matrix
        # each W is DxE where D is input embedding dimension and E is the output embedding dimension
        # the initialization value is equal to 1
        if self.W is None:
            self.W = np.ones((self.D, self.E))
        # SGD train the weights with T iteration using LR[t] as the learning rate for each t iteration
        Indices= list(range(0,N))
        for t in range(0,T):
            random.shuffle(Indices)
            for index in range(0,N):
                n= Indices[index] #randint(0,N-1) # pick a random sample n
                max= -1 # max rank
                max_j= -1 # max_j keeps the index of the max rank
                for j in range(0,len(classEmdes)): # iterate over all Y to find the one with the maximum rank
                    delta=0 if Y[n]==classEmdes[j] else 1 # 0-1 loss
                    comp=0 # compatibiliy with Y[j]
                    # trueLabelComp=0 # compatibiliy with Y[n](true label)
                    #aggregate compatabilities for all embeddings
                    inputP= np.dot(np.transpose(X[n]),self.W) # project input on joint embedding space
                    inputProj = l2_norm(inputP) # normalization
                    comp += np.dot(inputProj,classEmdes[j]) # compatability between input projection and Y[j][k]
                    loss = delta + comp #- trueLabelComp
                    if loss > max:
                        max = loss
                        max_j = j
                #update weights of all output embeddings
                if ((classEmdes[max_j]!= Y[n]) and (max_j != -1)):
                    tranX = np.array(X[n])[np.newaxis]
                    self.W = np.subtract(self.W, LR * np.dot(tranX.T,  np.array(classEmdes[max_j])[np.newaxis]))
                    self.W = np.add(self.W, LR * np.dot(tranX.T,  np.array(Y[n])[np.newaxis]))

    def predict(self,input,classesEmb):
        maxRank= -1
        maxRankIndex= -1
        for i in range(0,len(classesEmb)):
            rank=0
            inputP= np.dot(np.transpose(input),self.W) # project input on joint embedding space
            inputProj = l2_norm(inputP) # normalization
            rank+= np.dot(inputProj,classesEmb[i])
            if rank>maxRank:
                maxRank=rank
                maxRankIndex=i
        return {'score':maxRank,'label':classesEmb[maxRankIndex]}

    def test(self,X,Y,setClassEmdes):
        classesPositives =[]
        classesCount =[]
        classesPositives =[]
        classesCount =[]
        for i in range(0,len(setClassEmdes)):
            classesPositives.append(0)
            classesCount.append(0)
        for i in range(0,len(X)):
            classesCount[setClassEmdes.index(Y[i])]+=1
            res = self.predict(X[i],setClassEmdes)
            predictedClass = res['label']
            if predictedClass==Y[i]:
                index = setClassEmdes.index(predictedClass)
                classesPositives[index] +=1
        classesAccuracies=[]
        for i in range(0,len(classesPositives)):
            if not (classesCount[i]==0):
                classesAccuracies.append(float(float(classesPositives[i]*100)/classesCount[i]))
        acc = float(sum(classesAccuracies)/len(setClassEmdes))
        return acc

    def testPerImage(self,X,Y,classEmdes):
        positive=0 # number of positive predictions
        for i in range(0,len(X)): # prediction
            predictedClass= self.predict(X[i],classEmdes)
            if predictedClass==Y[i]:
                positive+=1 # increment positive predictions
        acc=float(float(positive)*100)/len(X)
        return acc

    def trainAndValidate(self,trX,trY,trSet,valX,valY,valSet):
        accuracies=[]
        W=[]
        for e in range(0, len(self.Eta)):
            first = True
            for t in range(0, len(self.T)):
                if first:
                    self.train(trX,trY,trSet,self.Eta[e],self.T[t])
                    first = False
                else:
                    self.train(trX, trY, trSet, self.Eta[e], self.T[t] - self.T[t - 1])
                # validation
                acc= self.test(valX,valY,valSet)
                accuracies.append([self.T[t],self.Eta[e],acc])
        return accuracies

    def retrainAndTest(self,trX,trY,trSet,valX,valY,valSet,teX,teY,teSet,bestEta,bestEp): # re-train with best settings
        #combine train&validation data
        X = np.concatenate([trX,valX],0)
        Y = []
        for i in range(0,len(trY)):
            Y.append(trY[i])
        for i in range(0,len(valY)):
            Y.append(valY[i])
        combSetClass = trSet + valSet
        self.train(X,Y, combSetClass,bestEta,bestEp)
        # test on testing set
        accPerClass= self.test(teX,teY,teSet)
        accPerImg= self.testPerImage(teX,teY,teSet)
        return {'accPerClass':accPerClass,'accPerImg':accPerImg}

    def distDataZeroShot(self, x,y, classesPartition):
        trSet = classesPartition[0]
        valSet = classesPartition[1]
        teSet = classesPartition[2]
        self.testSet = teSet
        tempEmb=[]
        for i in range(0,len(self.classEmdes)):
            tempEmb.append(self.classEmdes[i])
        teSet= self.embedClasses(teSet,tempEmb)
        trSet= self.embedClasses(trSet,tempEmb)
        valSet= self.embedClasses(valSet,tempEmb)
        trX,trY,valX,valY,teX,teY= [],[],[],[],[],[]
        self.testImages=[]
        for i in range(0,len(y)):
            if y[i] in trSet:
                trX.append(x[i])
                trY.append(y[i])
            elif y[i] in valSet:
                valX.append(x[i])
                valY.append(y[i])
            elif y[i] in teSet:
                teX.append(x[i])
                teY.append(y[i])
                self.testImages.append(self.images[i])
        return {'trX':trX,'trY':trY,'valX':valX,'valY':valY,'teX':teX,'teY':teY,'trSet':trSet,'valSet':valSet,'teSet':teSet}

    def embedClasses(self,tempY,yEmb):
        Y=[]
        for i in range(0,len(tempY)):
            Y.append(list(yEmb[tempY[i]]))
        return Y

    def runZeroShot(self, classesPartition):
        dist= self.distDataZeroShot(self.x, self.y, classesPartition)
        accuracies= self.trainAndValidate(dist['trX'],dist['trY'],dist['trSet'],dist['valX'],dist['valY'],dist['valSet'])
        bestEpoch, bestEta, _ = max(accuracies,key=lambda item:item[2])
        result= self.retrainAndTest(dist['trX'],dist['trY'],dist['trSet'],dist['valX'],dist['valY'],dist['valSet'],dist['teX'],dist['teY'],dist['teSet'],bestEta,bestEpoch)
        print('Val acc',accuracies)
        print('Test acc',np.average(np.array(result['accPerClass'])))
