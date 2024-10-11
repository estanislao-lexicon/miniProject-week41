# Asset Tracking 

This project is the start of an Asset Tracking database. It should have input possibilities from a user and print out functionality of user data.
Asset Tracking is a way to keep track of the company assets, like Laptops, Stationary computers, Phones and so on... 

All assets have an end of life which for simplicity reasons is 3 years. 


### Level 1

- [X] Create a console app that have classes and objects. For example like below with computers and phones.

Laptop Computers 
- MacBook
- Asus
- Lenovo

Mobile Phones 
- Iphone
- Samsung
- Nokia
 
You will need to:

- [X] create the appropriate properties and constructors for each object, 
    - [X] purchase date, 
    - [X] price, 
    - [X] model name etc. 


### Level 2

- [X] Create a program to create a list of assets (inputs) where the final result is to write the following to the console: 
- [X] Sorted list with Class as primary (computers first, then phones)
- [X] Then sorted by purchase date
- [X] Mark any item *RED* if purchase date is less than 3 months away from 3 years.


### Level 3

- [X] Add offices to the model: 
- [X] You should be able to place items in 3 different offices around the world (USA, Germany, Spain, UK and Sweden)
- [X] which will use the appropriate currency for that country. 
- [X] You should be able to input values in dollars and convert them to each currency (based on today's currency charts)

When you write the list to the console: 
- [X] Sorted first by office
- [X] Then Purchase date
- [X] Items *RED* if date less than 3 months away from 3 years
- [X] Items *Yellow* if date less than 6 months away from 3 years
- [X] Each item should have currency according to country