class Category:
    def __init__(self, name):
        self.ledger = []
        self.name = name
        self.total = 0

    def deposit(self, amount, description=None):
        self.total += amount
        if description == None:
            self.description = ""
        else:
            self.description = description
        self.ledger.append({"amount": amount, "description": self.description})

    def withdraw(self, amount, description=None):
        if amount > self.total:
            return False
        else:
            self.total -= amount
            if description == None:
                self.description = ""
            else:
                self.description = description
            self.ledger.append(
                {"amount": -amount, "description": self.description}
            )
            return True

    def get_balance(self):
        return self.total

    def transfer(self, amount, other_cat):
        if amount > self.total:
            return False
        else:
            self.total -= amount
            other_cat.total += amount
            print(other_cat)
            self.ledger.append(
                {
                    "amount": -amount,
                    "description": f"Transfer to {other_cat.name}",
                }
            )
            other_cat.ledger.append(
                {"amount": amount, "description": f"Transfer from {self.name}"}
            )
            return True

    def check_funds(self, amount):
        if amount > self.total:
            return False
        else:
            return True

    def __str__(self):
        title = self.name
        count = 0

        lines_list = []

        while len(title) < 30:
            title = "*" + title + "*"

        for amount in self.ledger:
            amount = self.ledger[count]["amount"]
            amount = format(amount, ".2f")
            desc = self.ledger[count]["description"]
 
            while len(amount) < 7:
                amount = " " + amount
       
            while len(desc) < 23:
                desc += " "

            else:
                desc = desc[:23]

            lines_list.append(desc + amount)
            count += 1

        lines_print = "\n".join(lines_list)

        total = format(self.total, ".2f")
        total = f"Total: {self.total}"
        count = 0
        display = title + "\n" + lines_print + "\n" + total

        return display

def create_spend_chart(categories):

    title = "Percentage spent by category"
    chart = ""
    cats = []
    withdraws = []
    withdraw_percentage = []
    vertical_cats = ""

    for category in categories:
        cats.append(category.name)

        withdrawn_amount = 0

        for action in category.ledger:
            if action["amount"] < 0:
                withdrawn_amount -= action["amount"]

        withdraws.append(withdrawn_amount)

    for number in range(len(withdraws)):
        withdraw_percentage.append(withdraws[number] / sum(withdraws) * 100)

    for number in range(100, -10, -10):
 
        chart += str(number).rjust(3, " ") + "|"

        for num in withdraw_percentage:
            if num >= number:
                chart += " o "
            else:
                chart += "   "
        chart += " \n"

    chart += "    " + "-" * len(cats) * 3 + "-" + "\n"

    longest = max(len(name.name) for name in categories)

    for level in range(longest):

        vertical_cats += "    "

        for name in categories:
            if level < len(name.name):
                vertical_cats += " " + name.name[level] + " "
            else:
                vertical_cats += "   "
        vertical_cats += " \n"

    chart += vertical_cats.rstrip() + "  "

    display = title + "\n" + chart
    return display
  
