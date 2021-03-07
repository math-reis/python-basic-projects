from tkinter import*
import math
import tkinter.messagebox

root = Tk()
root.title("Scientific Calculator")
root.configure(background="powder blue")
root.resizable(width=False, height=False)
root.geometry("480x568+30+30")

calc = Frame(root)
calc.grid()


class Calc:
    def __init__(self):
        self.total = 0
        self.current = ''
        self.input_value = True
        self.check_sum = False
        self.op = ''
        self.result = False

    def number_enter(self, num):
        self.result = False
        first_num = txtDisplay.get()
        second_num = str(num)
        if self.input_value:
            self.current = second_num
            self.input_value = False
        else:
            if second_num == ".":
                if second_num in first_num:
                    return
            self.current = first_num + second_num
        self.display(self.current)

    def operation(self, op):
        self.current = float(self.current)
        if self.check_sum:
            self.valid_function()
        elif not self.result:
            self.total = self.current
            self.input_value = True
        self.check_sum = True
        self.op = op
        self.result = False

    def clear_entry(self):
        self.result = False
        self.current = "0"
        self.display(0)
        self.input_value = True

    def all_clear_entry(self):
        self.clear_entry()
        self.total = 0

    def squared(self):
        self.result = False
        self.current = math.sqrt(float(txtDisplay.get()))
        self.display(self.current)

    def valid_function(self):
        if self.op == "add":
            self.total += self.current
        if self.op == "sub":
            self.total -= self.current
        if self.op == "multi":
            self.total *= self.current
        if self.op == "divide":
            self.total /= self.current
        if self.op == "mod":
            self.total %= self.current
        self.input_value = True
        self.check_sum = False
        self.display(self.total)

    def maths_pm(self):
        self.result = False
        self.current = -(float(txtDisplay.get()))
        self.display(self.current)

    def sum_of_total(self):
        self.result = True
        self.current = float(self.current)
        if self.check_sum:
            self.valid_function()
        else:
            self.total = float(txtDisplay.get())

    def pi(self):
        self.result = False
        self.current = math.pi
        self.display(self.current)

    def cos(self):
        self.result = False
        self.current = math.cos(float(txtDisplay.get()))
        self.display(self.current)

    def tan(self):
        self.result = False
        self.current = math.tan(float(txtDisplay.get()))
        self.display(self.current)

    def sin(self):
        self.result = False
        self.current = math.sin(float(txtDisplay.get()))
        self.display(self.current)

    def tau(self):
        self.result = False
        self.current = math.tau
        self.display(self.current)

    def cosh(self):
        self.result = False
        self.current = math.cosh(float(txtDisplay.get()))
        self.display(self.current)

    def tanh(self):
        self.result = False
        self.current = math.tanh(float(txtDisplay.get()))
        self.display(self.current)

    def sinh(self):
        self.result = False
        self.current = math.sinh(float(txtDisplay.get()))
        self.display(self.current)

    def log(self):
        self.result = False
        self.current = math.log(float(txtDisplay.get()))
        self.display(self.current)

    def exp(self):
        self.result = False
        self.current = math.exp(float(txtDisplay.get()))
        self.display(self.current)

    def e(self):
        self.result = False
        self.current = math.e
        self.display(self.current)

    def log2(self):
        self.result = False
        self.current = math.log2(float(txtDisplay.get()))
        self.display(self.current)

    def degrees(self):
        self.result = False
        self.current = math.degrees(float(txtDisplay.get()))
        self.display(self.current)

    def acosh(self):
        self.result = False
        self.current = math.acosh(float(txtDisplay.get()))
        self.display(self.current)

    def asinh(self):
        self.result = False
        self.current = math.asinh(float(txtDisplay.get()))
        self.display(self.current)

    def log10(self):
        self.result = False
        self.current = math.log10(float(txtDisplay.get()))
        self.display(self.current)

    def log1p(self):
        self.result = False
        self.current = math.log1p(float(txtDisplay.get()))
        self.display(self.current)

    def expm1(self):
        self.result = False
        self.current = math.expm1(float(txtDisplay.get()))
        self.display(self.current)

    def l_gamma(self):
        self.result = False
        self.current = math.lgamma(float(txtDisplay.get()))
        self.display(self.current)

    @staticmethod
    def display(value):
        txtDisplay.delete(0, END)
        txtDisplay.insert(0, value)


added_value = Calc()

txtDisplay = Entry(calc, font=('arial', 20, 'bold'), bg="powder blue", bd=30, width=28, justify=RIGHT)
txtDisplay.grid(row=0, column=0, columnspan=4, pady=1)
txtDisplay.insert(0, "0")

numberpad = "789456123"
i = 0
btn = []
for j in range(2, 5):
    for k in range(3):
        btn.append(Button(calc, width=6, height=2, font=('arial', 20, 'bold'), bd=4, text=numberpad[i]))
        btn[i].grid(row=j, column=k, pady=1)
        btn[i]["command"] = lambda x = numberpad[i]: added_value.number_enter(x)
        i += 1

# Standard Calculator Buttons ==========================================================================================

lblDisplay = Label(calc, text="Scientific Calculator", font=('arial', 20, 'bold'), justify=CENTER)
lblDisplay.grid(row=0, column=4, columnspan=4)

# ======================================================================================================================

btnClear = Button(calc, text=chr(67), width=6, height=2, font=('arial', 20, 'bold'), bd=4,
                  bg="powder blue", command=added_value.clear_entry).grid(row=1, column=0, pady=1)

btnAllClear = Button(calc, text=chr(67) + chr(69), width=6, height=2, font=('arial', 20, 'bold'), bd=4,
                     bg="powder blue", command=added_value.all_clear_entry).grid(row=1, column=1, pady=1)

btnSq = Button(calc, text="√", width=6, height=2, font=('arial', 20, 'bold'), bd=4,
               bg="powder blue", command=added_value.squared).grid(row=1, column=2, pady=1)

btnAdd = Button(calc, text="+", width=6, height=2, font=('arial', 20, 'bold'), bd=4,
                bg="powder blue", command=lambda: added_value.operation("add")).grid(row=1, column=3, pady=1)

# ======================================================================================================================

btnSub = Button(calc, text="-", width=6, height=2, font=('arial', 20, 'bold'), bd=4,
                bg="powder blue", command=lambda: added_value.operation("sub")).grid(row=2, column=3, pady=1)

btnMult = Button(calc, text="*", width=6, height=2, font=('arial', 20, 'bold'), bd=4,
                 bg="powder blue", command=lambda: added_value.operation("multi")).grid(row=3, column=3, pady=1)

btnDiv = Button(calc, text=chr(247), width=6, height=2, font=('arial', 20, 'bold'), bd=4,
                bg="powder blue", command=lambda: added_value.operation("divide")).grid(row=4, column=3, pady=1)

# ======================================================================================================================

btnZero = Button(calc, text="0", width=6, height=2, font=('arial', 20, 'bold'), bd=4,
                 bg="powder blue", command=lambda: added_value.number_enter(0)).grid(row=5, column=0, pady=1)

btnDot = Button(calc, text=".", width=6, height=2, font=('arial', 20, 'bold'), bd=4,
                bg="powder blue", command=lambda: added_value.number_enter(".")).grid(row=5, column=1, pady=1)

btnPM = Button(calc, text=chr(177), width=6, height=2, font=('arial', 20, 'bold'), bd=4,
               bg="powder blue", command=added_value.maths_pm).grid(row=5, column=2, pady=1)

btnEquals = Button(calc, text="=", width=6, height=2, font=('arial', 20, 'bold'), bd=4,
                   bg="powder blue", command=added_value.sum_of_total).grid(row=5, column=3, pady=1)

# Scientific Calculator Buttons ========================================================================================

btnPi = Button(calc, text="π", width=6, height=2, font=('arial', 20, 'bold'),
               bd=4, bg="powder blue", command=added_value.pi).grid(row=1, column=4, pady=1)

btnCos = Button(calc, text="cos", width=6, height=2, font=('arial', 20, 'bold'),
                bd=4, bg="powder blue", command=added_value.cos).grid(row=1, column=5, pady=1)

btnTan = Button(calc, text="tan", width=6, height=2, font=('arial', 20, 'bold'),
                bd=4, bg="powder blue", command=added_value.tan).grid(row=1, column=6, pady=1)

btnSin = Button(calc, text="sin", width=6, height=2, font=('arial', 20, 'bold'),
                bd=4, bg="powder blue", command=added_value.sin).grid(row=1, column=7, pady=1)

# ======================================================================================================================

btn2Pi = Button(calc, text="2π", width=6, height=2, font=('arial', 20, 'bold'),
                bd=4, bg="powder blue", command=added_value.tau).grid(row=2, column=4, pady=1)

btnCosh = Button(calc, text="cosh", width=6, height=2, font=('arial', 20, 'bold'),
                 bd=4, command=added_value.cosh).grid(row=2, column=5, pady=1)

btnTanh = Button(calc, text="tanh", width=6, height=2, font=('arial', 20, 'bold'),
                 bd=4, command=added_value.tanh).grid(row=2, column=6, pady=1)

btnSinh = Button(calc, text="sinh", width=6, height=2, font=('arial', 20, 'bold'),
                 bd=4, command=added_value.sinh).grid(row=2, column=7, pady=1)

# ======================================================================================================================

btnLog = Button(calc, text="log", width=6, height=2, font=('arial', 20, 'bold'),
                bd=4, bg="powder blue", command=added_value.log).grid(row=3, column=4, pady=1)

btnExp = Button(calc, text="Exp", width=6, height=2, font=('arial', 20, 'bold'),
                bd=4, command=added_value.exp).grid(row=3, column=5, pady=1)

btnMod = Button(calc, text="Mod", width=6, height=2, font=('arial', 20, 'bold'),
                bd=4, command=lambda: added_value.operation("mod")).grid(row=3, column=6, pady=1)

btnE = Button(calc, text="e", width=6, height=2, font=('arial', 20, 'bold'),
              bd=4, command=added_value.e).grid(row=3, column=7, pady=1)

# ======================================================================================================================

btnLog2 = Button(calc, text="log2", width=6, height=2, font=('arial', 20, 'bold'),
                 bd=4, bg="powder blue", command=added_value.log2).grid(row=4, column=4, pady=1)

btnDeg = Button(calc, text="deg", width=6, height=2, font=('arial', 20, 'bold'),
                bd=4, command=added_value.degrees).grid(row=4, column=5, pady=1)

btnAcosh = Button(calc, text="acosh", width=6, height=2, font=('arial', 20, 'bold'),
                  bd=4, command=added_value.acosh).grid(row=4, column=6, pady=1)

btnAsinh = Button(calc, text="asinh", width=6, height=2, font=('arial', 20, 'bold'),
                  bd=4, command=added_value.asinh).grid(row=4, column=7, pady=1)

# ======================================================================================================================

btnLog10 = Button(calc, text="log10", width=6, height=2, font=('arial', 20, 'bold'),
                  bd=4, bg="powder blue", command=added_value.log10).grid(row=5, column=4, pady=1)

btnLog1p = Button(calc, text="log1p", width=6, height=2, font=('arial', 20, 'bold'),
                  bd=4, bg="powder blue", command=added_value.log1p).grid(row=5, column=5, pady=1)

btnExpm1 = Button(calc, text="expm1", width=6, height=2, font=('arial', 20, 'bold'),
                  bd=4, bg="powder blue", command=added_value.expm1).grid(row=5, column=6, pady=1)

btnLgamma = Button(calc, text="lgamma", width=6, height=2, font=('arial', 20, 'bold'),
                   bd=4, bg="powder blue", command=added_value.l_gamma).grid(row=5, column=7, pady=1)

# Menu and Functions ===================================================================================================


def standard():
    root.resizable(width=False, height=False)
    root.geometry("480x568+30+30")


def scientific():
    root.resizable(width=False, height=False)
    root.geometry("944x568+30+30")


def i_exit():
    i_exit = tkinter.messagebox.askyesno("Scientific Calculator", "Confirm if you want to exit.")
    if i_exit > 0:
        root.destroy()
        return


menu_bar = Menu(calc)

file_menu = Menu(menu_bar, tearoff=0)
menu_bar.add_cascade(label="File", menu=file_menu)
file_menu.add_command(label="Standard", command=standard)
file_menu.add_command(label="Scientific", command=scientific)
file_menu.add_separator()
file_menu.add_command(label="Exit", command=i_exit)

edit_menu = Menu(menu_bar, tearoff=0)
menu_bar.add_cascade(label="Edit", menu=edit_menu)
edit_menu.add_command(label="Cut")
edit_menu.add_command(label="Copy")
edit_menu.add_separator()
edit_menu.add_command(label="Paste")

help_menu = Menu(menu_bar, tearoff=0)
menu_bar.add_cascade(label="Help", menu=help_menu)
help_menu.add_command(label="View Help")

root.config(menu=menu_bar)
root.mainloop()
