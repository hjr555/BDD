# .NET Core Gherkin workshop

> A short workshop to run through setting up some tests using gherkin.

## Prepare your chosen IDE

For Visual Studio 2019, you should install the [SpecFlow for Visual Studio 2019](https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlowTeam.SpecFlowForVisualStudio) extension.  

For VS Code, you should install the [Cucumber (Gherkin) Full Support](https://marketplace.visualstudio.com/items?itemName=alexkrechik.cucumberautocomplete) extension. This will require some configuration (out of scope).

## Prepare your working folder

1. Create a folder called `BDD`
2. `cd` into this folder, and run the following commands:  
    `dotnet new xunit -n Tests`  
	`dotnet new classlib -n Calculator`

3. `cd` into the `Tests` folder, and run the following commands:  
	`dotnet add package Xunit.Gherkin.Quick`  
	`dotnet add package xunit.runner.console`  
	`dotnet add reference ..\Calculator\Calculator.csproj`  

4. `cd` up one level back into the BDD folder, and run the following commands:  
	`dotnet new sln`  
	`dotnet sln add Calculator\Calculator.csproj`  
	`dotnet sln add Tests\Tests.csproj`  

5. At this point, you may wish to create a `.gitignore` file with the following contents:  
    `bin`  
    `obj`  
    `.vs`  
    `.vscode`

6. If you wish to create a new git repository, run the following commands: 
    `git init`  
    `git add .`  
    `git commit -m "My initial commit"`  

## Create the features & scenarios

7. Either open up Visual Studio 2019, and open `BDD.sln`, or enter the following command to open VS Code:  
    `code .`  

8. The following files can now be deleted:  
    `Calculator\Class1.cs`  
    `Tests\UnitTest1.cs`  

9. Create a new folder for our calculator feature tests:   
    `Tests\Addition`  

10. Create a new feature file, for our test:   
    `Tests\Addition\AddTwoNumbers.feature`  

11. Paste in the gherkin syntax below.:  
	```gherkin
    Feature: AddTwoNumbers
	        In order to learn Math
	        As a regular human
	        I want to add two numbers using Calculator
	    
        Scenario: Add two numbers
	        Given I choose 12 as first number
	        And I choose 15 as second number
	        When I press add
	        Then the result should be 27 on the screen
    ```
	
12. We have to ensure the `*.feature` files are copied into our output directory on build. To do this, add the following just above the closing `</Project>` tag in your `Tests.csproj` file:  
    ```xml
	  <ItemGroup>
	    <None Update=".\**\*.feature">
	      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
	    </None>
	  </ItemGroup>
    ```
13. There is a missing link. We need to let xunit know how to understand the `AddTwoNumbers.feature` file that we have just added.  
This requires a C# class with the same name as the feature.  Create a new class with the same name:  
`Tests\AddTwoNumbers.cs`

14. There is a lot going on in this next piece of code. You will see that the class extends the `Feature` class, and uses attributes to declare what to expect inside the `*.feature` files.  
Copy and paste this into your new `AddTwoNumbers.cs` class:  
    ```csharp
    using Xunit;
    using Xunit.Gherkin.Quick;

    [FeatureFile("./Addition/AddTwoNumbers.feature")]
    public sealed class AddTwoNumbers : Feature
    {
        private readonly Calculator _calculator = new Calculator();

        [Given(@"I choose (\d+) as first number")]
        public void I_choose_first_number(int firstNumber)
        {
            _calculator.SetFirstNumber(firstNumber);
        }

        [And(@"I choose (\d+) as second number")]
        public void I_choose_second_number(int secondNumber)
        {
            _calculator.SetSecondNumber(secondNumber);
        }

        [When(@"I press add")]
        public void I_press_add()
        {
            _calculator.AddNumbers();
        }

        [Then(@"the result should be (\d+) on the screen")]
        public void The_result_should_be_z_on_the_screen(int expectedResult)
        {
            var actualResult = _calculator.Result;

            Assert.Equal(expectedResult, actualResult);
        }
    }
    ```

15. In your Calculator project, add a new file, called `Calculator.cs`, and paste the following code:  
    ```csharp
    public class Calculator
    {
        public int Result { get; set; }

        public void SetFirstNumber(int firstNumber)
        {
        }

        public void SetSecondNumber(int secondNumber)
        {
        }

        public void AddNumbers()
        {
        }
    }
    ```

## Build & run the tests

16. From the command line, make sure you are in the `BDD` folder (use `cd` to change, if not), and run the following command:  
    `dotnet build`

17. You should now be able to run your new test. Run the following command (it should fail!):  
    `dotnet test`

## Red -> Green -> Refactor

18. In `Calculator.cs`, change your `AddNumbers()` method to the following:  
    ```csharp
    public void AddNumbers()
    {
        Result = 27;
    }
    ```

19. Now, re-run your test. It should pass:  
    `dotnet test`

20. This is clearly not a useful calculator.  
Extend the feature with additional scenarios, using the above approach.  
Use `dotnet test` to re-run your tests and confirm your calculator handles a range of scenarios.

## Extend the behaviours

21. Create a new feature, to support multiplication, with multiple scenarios that initially fail when `dotnet test` is run.

22. Complete the exercise by extending `Calculator.cs` until all tests pass.
