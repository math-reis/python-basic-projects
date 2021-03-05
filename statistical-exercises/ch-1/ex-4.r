# 4. R contains a number of built-in data sets that are available for beginning programmers to work on. 
# (To see a list of these free data sets, simply enter data()) at the R prompt in the Console.) For example, 
# one of the data sets is named LakeHuron (named after the Great Lake situated on the Canadian-US border). 
# To learn a bit about this data set, enter at the R prompt ?LakeHuron. When we do this, a page opens to describe 
# the data set, informing us that the LakeHuron data consists of "Annual Measurements of the level, in feet, of 
# Lake Huron, 1875 - 1972." The following questions concern the the LakeHuron data set.

# (a) Use function head(LakeHuron,3) to examine the first three elements of data set LakeHuron. 
# (Use function head(LakeHuron,n) to display the first n data items.)



# (b) Are any data missing? Use function length(LakeHuron).



# (c) What is the lowest level (in feet) of Lake Huron during the 1875-1972 period?



# (d) What is the highest level of Lake Huron during the same period?



# (e) What is the mean level of Lake Huron during this period?



# (f) What is the median level?




# Comment1. Use head() function to view the first three observations.

# head(LakeHuron, 3)

## [1] 580.38 581.86 580.97

# Comment2. Does the data set contain one measurement for each year

# from 1875 to 1972, or does it include any missing data values or

# years? (Since there are 98 years from 1875 to 1972, there should be

# 98 elements in LakeHuron if there are no missing data.)

# length(LakeHuron)

## [1] 98

# Comment3. Use function min() to find lowest level of LakeHuron.

# min(LakeHuron)

## [1] 575.96

# Comment4. Use function max() for the highest level of LakeHuron.

# max(LakeHuron)

## [1]  581.86

# Comment5. Use function mean() to find mean.

# mean(LakeHuron)

## [1] 579.0041

#Comment6. Use function median() to find median.

# median(LakeHuron)

## [1] 579.12
