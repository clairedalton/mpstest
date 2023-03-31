# MYP Senior Engineer Test

Build an investment app API. 

It should allow you to:
* Add an investment
    
    * An investment should include:
        * Name
        * Start Date
        * Interest Type: Simple or Compound
        * Interest Rate
        * Principle Amount
    * The name of an investment should be unique
* Update an investment
* Delete an investment
* Fetch Investment
    * Returning:
        * Name
        * Start Date
        * Interest Type: Simple or Compound
        * Interest Rate
        * Principle Amount
        * Current value of the investment rounded to the nearest month
    
    
    ```
    Acceptance Criteria 1
    GIVEN an investment is of type simple
    WHEN interest is calculated
    THEN a value is returned equal to  A = P(1 + rt)
    AND the period is rounded to the nearest month

    Where:
    A = Total Accrued Amount (principal + interest)
    P = Principal Amount
    I = Interest Amount
    r = Rate of Interest per year in decimal
    t = Time Period involved in months or years
    See https://www.calculatorsoup.com/calculators/financial/simple-interest-plus-principal-calculator.php

    Acceptance Criteria 2
    GIVEN an investment is of type compound
    WHEN interest is calculated
    THEN a value is returned equal to  A = P(1 + r/n)nt
    AND the compounding perdiod is Monthly
    AND the period is rounded to the nearest month
    
    Where:
    A = Accrued amount (principal + interest)
    P = Principal amount
    r = Annual nominal interest rate as a decimal
    n = number of compounding periods per unit of time
    t = time in decimal years; e.g., 6 months is calculated as 0.5 years. 
    See https://www.calculatorsoup.com/calculators/financial/compound-interest-calculator.php
    ```
* Authentication is not needed
* In memory DB is acceptable
* It should be extensible
* Quality should be high



Instructions:
* Push your solution to your github repo
* Please use your commits to tell a story (i.e. not one big commit) 
* Be ready to answer questions about your choices
* Send an email with:
    * A link to your repo
    * Instructions on how to use the API to add, delete, update and fetch an investment
    * Any pieces of functionality you decided not to complete with reasons behind the decision








