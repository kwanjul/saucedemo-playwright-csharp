Feature: Login

  Scenario: Successful login with valid credentials
    Given I am on the Sauce Demo login page
    When I log in with valid credentials
    Then I should see the inventory page

  Scenario: Unsuccessful login with invalid credentials
    Given I am on the Sauce Demo login page
    When I log in with invalid credentials
    Then I should see a login error message