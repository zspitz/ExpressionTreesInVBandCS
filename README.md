# Sample code for _Expression Trees in Visual Basic and C#_

This repo contains accompanying samples for _Expression Trees in Visual Basic and C#_, an MSDN Magazine article which is scheduled to appear in the September 2019 issue.

C# sample code is in projects whose name starts with `CSharp`; Visual Basic sample projects start with `VisualBasic`.

The project structure is as follows:

| Project name | Description |
| --- | --- |
| `_Shared` | Contains shared code used by multiple samples, e.g. data classes and extensions |
| `CSharp`<br/>`VisualBasic` | Short samples encapsulated in a single method |
| `CSharp_SetColumn` | Implements declaratively setting columns on a WPF DataGrid in code, using expression trees <br/> Also uses an expression tree visitor to generate a binding path for the given expression. |

## TODO

* `Like` operator tree visitor
* DLR CallSite
