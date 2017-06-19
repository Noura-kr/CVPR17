index = 0
partitions =  [[[8, 7, 3, 4, 13, 11, 2, 9], [1, 10, 6], [12, 5, 0]],[[13, 11, 8, 5, 6, 4, 0, 9], [10, 3, 12], [1, 2, 7]],[[13, 4, 12, 6, 9, 3, 0, 5], [11, 10, 8], [7, 2, 1]],[[7, 11, 6, 2, 13, 1, 8, 0], [12, 9, 5], [4, 3, 10]],[[7, 2, 13, 12, 8, 6, 4, 9], [3, 10, 0], [5, 11, 1]],[[0, 3, 13, 12, 5, 1, 8, 2], [9, 6, 7], [4, 10, 11]],[[7, 0, 5, 8, 9, 11, 10, 12], [3, 2, 1], [13, 6, 4]],[[7, 6, 10, 8, 5, 0, 2, 1], [13, 11, 4], [3, 12, 9]],[[0, 8, 2, 12, 1, 10, 4, 3], [5, 7, 13], [11, 6, 9]],[[11, 10, 5, 1, 12, 9, 3, 8], [0, 4, 2], [7, 13, 6]]]

part = partitions[index]
inputFile = '/Users/Nour/Desktop/Ready_CVPR17/Data/Image_features/CUB-VW.npy'
outputDir = '/Users/Nour/Desktop/Ready_CVPR17/Data/Gaze_Embeddings/CUB-VW/GH_combined_avg.npy'

yEmb = np.load(outputDir)
classesNum=yEmb.shape[0]


sje = SJE(inputFile, yEmb, [1,5,25], [0.1,1], classesNum)
sje.runZeroShot(part)
