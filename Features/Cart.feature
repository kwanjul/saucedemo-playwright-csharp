Feature: Cart

  Scenario: Add a product to the shopping cart
    Given I am logged into Sauce Demo
    When I add Sauce Labs Backpack to the shopping cart
    Then the shopping cart should contain Sauce Labs Backpack
