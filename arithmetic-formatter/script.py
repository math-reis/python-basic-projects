def arithmetic_arranger(problems, show = False):
  firstAnswer = ""
  operators = ""
  dashlines = ""
  secondAnswer = ""
  sumup = ""
  answer = ""
  sum = ""

  if(len(problems) > 5):
    return "Error: Too many problems."

  for problem in problems:
    #spliting the strings into a list
    firstNumber = problem.split(" ")[0]
    operators = problem.split(" ")[1]
    secondNumber = problem.split(" ")[2]

    # check the length of the number, max 4 digits
    if (len(firstNumber) > 4 or len(secondNumber) > 4):
      return "Error: Numbers cannot be more than four digits."

    # check the input as valid digits
    if not firstNumber.isnumeric() or not secondNumber.isnumeric():
      return "Error: Numbers must only contain digits."

    # check for the correct form of operators
    
    if (operators == '+' or operators == '-'):
      if operators == "+": 
        sum = str(int(firstNumber) + int(secondNumber))
      if operators == "-":
        sum = str(int(firstNumber) - int(secondNumber))
    else: 
      return "Error: Operator must be '+' or '-'."

    length = max(len(firstNumber) , len(secondNumber)) + 2
    fisrtline = str(firstNumber).rjust(length)
    secondline = operators + str(secondNumber.rjust(length - 1))
    sltn = str(sum.rjust(length))
    
    dashline = ""
    for dash in range(length):
      dashline += "-"

    if problem != problems[-1]: 
      firstAnswer += fisrtline + '    '
      secondAnswer += secondline + '    '
      dashlines += dashline + '    '
      sumup += sltn + '    '
    else: 
      firstAnswer += fisrtline
      secondAnswer += secondline 
      dashlines += dashline 
      sumup += sltn  

  if show: 
    answer = firstAnswer + "\n" + secondAnswer + "\n" + dashlines + "\n" + sumup
  else:
    answer = firstAnswer + "\n" + secondAnswer + "\n" + dashlines    
  return answer
