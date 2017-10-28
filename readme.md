# Gaze Embeddings for Zero-Shot Image Classification

![alt text](http://www.mpi-inf.mpg.de/fileadmin/_processed_/csm_gaze_teaser_5963385378.png)

Code and gaze data for CVPR17 [paper] 
### Data
We provide the follwoing data for three different datasets (CUB-VW, CUB-SW, and PETS) 
- Gaze raw data
- Gaze-based class embeddings of three types GH, GFG, and GFS
- Image features extracted from [GoogLeNet] using Caffe
### Code
We provide two projects:
- Gaze data collection experiment written in C# using [Tobii TX300] Eye tracker SDK. The experiment steps are described and explained in the [paper]
- Structured Joint Embedding for Zero-shot learning implementation using python from [Evaluation of Output Embeddings for Fine-Grained Image Classification] paper


[paper]: <https://arxiv.org/abs/1611.09309>
[GoogLeNet]: <https://github.com/BVLC/caffe/tree/master/models/bvlc_googlenet>
[Tobii TX300]: <https://www.tobiipro.com/product-listing/tobii-pro-tx300/>
[Evaluation of Output Embeddings for Fine-Grained Image Classification]: <https://arxiv.org/abs/1409.8403>
