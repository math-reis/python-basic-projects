# install pyshorteners

import pyshorteners

link = input("Insert the link to be shortened: ")

shortener = pyshorteners.Shortener()

x = shortener.tinyurl.short(link)

print(x)
