# Test Notes

I've spent just over 2 hours on this, just getting some of the more obvious things.

The main focus was making the existing code more easily testable and a little less
fragile:

- Interest rate calculations have been extracted from `Investment`, and have
  their own unit tests
- When calculating the current value of an investment, the current time is now
  passed in, allowing for simpler testing.
- Tests are added for `Investment` to verify the correct values are being calculated
  based on the given time. Renamed to `GetValueAt`.
- A clock implementation is passed in to the `InvestmentController`, allowing for
  tests that rely on the current time.
- Additional tests have been added to `TestInvestmentController` to test investments
  that start in the future.
- The `InterestType` is now an Enum, and can not represent any value other than `Simple`
  and `Compound`.
- The exposed API now has separate types for `AddInvestmentRequest` and `InvestmentResponse`.
  This allows separating the API model from the internal data model, although they are
  currently almost identical.
- Added validation attributes to `AddInvestmentRequest`, preventing invalid requests from
  being accepted by the framework.
- Removed CurrentValue from `Investment`, added to `InvestmentResponse` instead. It is filled
  by the mapper method using `GetValueAt`, and no longer needs to be updated when the investment
  is created or updated.

I have not made any major modifications to the `InvestmentController` and none to the EF
code. Since this is a simple CRUD controller, it's fine for now.

As a future improvement, it might be worth splitting the `InvestmentController` into a service,
which handles all the business logic, and a controller that purely handles the HTTP API. As
simple as this is, it does not seem like a high priority here. If Investments were to evolve
to be more than a simple data repository, it would be a good idea.

One major notable issue - the existing code does not handle timezones. This works OK for a
simple test application. If we were deploying this as a production application, we would need
to handle timezones explicitly.

I haven't looked at that, but I would likely have a timezone setting somewhere (possibly in
`appsettings.json`), and ensure calculations are done in the correct timezone. Using
NodaTime may generally be advisable, since it has types specifically for calendar dates in
a specific timezone.

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








