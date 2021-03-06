# 2. Enter the following small data set directly into the R Workspace, and name it E1_1: 81, 17, 7, 55, 2, 98, 71, 47, 19, 8, 3, 10, 28, 65, 80. 
# Check to make sure that E1_1 contains these elements, and answer the following questions.

# Comment1. Use the c() function to create object E1_1.
# E1_1 <- c(81, 17, 7, 55, 2, 98, 71, 47, 19, 8, 3, 10, 28, 65, 80)
# Comment2. Examine contents of E1_1.
# E1_1
## [1] 81 17 7 55 2 98 71 47 19 8 3 10 28 65 80

# (a) The median of a (sorted) data set is de ned as a value that cuts the data set exactly in two, leaving the same number of data items below as above 
# this value. What is the median of E1_1? Hint: use the sort() function to rank order all data values in E1_1, from lowest to highest. Confirm that the 
# value of the median of E1_1 is the same as when you use the median() function.

E1_1 <- c(81, 17, 7, 55, 2, 98, 71, 47, 19, 8, 3, 10, 28, 65, 80)
x <- sort(E1_1)
length(x)
## [1] 15
(15 + 1) / 2
## [1] 8
x[8]
## [1] 28

# Prove
median(x)
## [1] 28

# (b) Using the max() and min() functions, find the maximum and minimum values of E1_1. Also, using the sum() and mean() functions, find the sum of all 
# the data values as well as the mean of E1_1.

max(x)
## [1] 98
min(x)
## [1] 2
sum(x)
## [1] 591
mean(x)
## [1] 39.4

# (c) Count the number of data values in E1_1. Although it is clear that there are 15 elements, the length() function can be used when we want to know the 
# number of elements contained in data sets of unknown size.

length(x)
## [1] 15
