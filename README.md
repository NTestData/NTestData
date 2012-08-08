# NTestData

## Natural Test Data for .NET

It's a very subtle test data factory with flexible capabilities to customize created objects. Permanent customizations per type can be defined in order to be always applied on-the-fly to objects being created. At the same time any number of additional ad-hoc customizations can be applied in particular test case.

Generally the syntax is extremely simple. Since every customization is an `Action<T>` where `T` is the type of created object. This leads to the very clean and expressive test code.

Note that this micro-framework is NOT opposed to data generation libraries or frameworks such as NBuilder or AutoPoco but can nicely complement them in most of the cases.

## Few examples

Imagine we're required to implement some application in demographic area. And let's say there's some complex algorithmic 

    var family = TestData.Create<Family>(
       From("Canada"),
       Parents(Dad("Alan", 43), Mom("Betty", 39)),
       Kids(Son("Chris", ), Daughter("Dolly")));

## Advanced usage

Actually it's not only library but sort of framework with explicitly defined core pieces which allows you to build your own custom implementation