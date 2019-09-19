# Sample code for _Expression Trees in Visual Basic and C#_

This repo contains accompanying samples for [_Expression Trees in Visual Basic and C#_](https://msdn.microsoft.com/en-us/magazine/mt833513), an MSDN Magazine article which appeared in the September 2019 issue.

All the samples are available in both a C# version and a VB.NET version (except for the sample demonstrating the usage of the `Like` operator rewrite, because C# doesn't have the `Like` operator).

The samples, in order of appearance in the article, are as follows:

| Figure | Name/Description | Location |
| --- | --- | --- |
| **2** | **Expression Tree Objects Using Object Notation**<br/>Shows an expression tree using object and collection initialization syntax | [`CSharp`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/CSharp/Program.cs#L37)<br/>[`VisualBasic`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/VisualBasic/Program.vb#L32) |
| **3** | **NodeType, Value, Name properties**<br/>Shows the properties relevant to understanding the structure of an expression tree | [`CSharp`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/CSharp/Program.cs#L42)<br/>[`VisualBasic`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/VisualBasic/Program.vb#L37) |
| **5** | **Blocks, Assignments and Statements in Expression Trees**<br/>Constructs an expression tree using factory methods, interspersed with the corresponding code in comments | [`CSharp`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/CSharp/Program.cs#L54)<br/>[`VisualBasic`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/VisualBasic/Program.vb#L49) |
| **6** | **Visualization of Final Expression Tree**<br/>Demonstrates what an expression tree looks like before and after a call to `Queryable.Where` | [`CSharp`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/CSharp/Program.cs#L100)<br/>[`VisualBasic`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/VisualBasic/Program.vb#L78) |
| **7** | **Requests Using the Simple.OData.Client Library and Expression Trees** | [`CSharp`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/CSharp/Program.cs#L114)<br/>[`VisualBasic`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/VisualBasic/Program.vb#L90) |
| | **`GetMethod` -- Reflection by Example**<br/>Uses an expression tree to target a specific MethodInfo instead of reflection | [`CSharp`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/CSharp/Program.cs#L136)<br/>[`VisualBasic`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/VisualBasic/Program.vb#L109) |
| | **Grid Column Configuration**<br/>Declaratively set the columns of a WPF `DataGrid` using an appropriate expression | [`CSharp_SetColumns`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/CSharp_SetColumns/MainWindow.xaml.cs)<br/>[`VisualBasic_SetColumns`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/VisualBasic_SetColumns/MainWindow.xaml.vb) |
| **8** | **ExpressionTreeVisitor Replacing Visual Basic's `Like` with `DbFunctions.Like`** | [`CSharp_LikeVisitor`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/CSharp_LikeVisitor/LikeVisitor.cs)<br/>[`VisualBasic_LikeVisitor`](https://github.com/zspitz/ExpressionTreesInVBandCS/blob/master/VisualBasic_LikeVisitor/LikeVisitor.vb) |

## Description of the project structure 

Projects whose name starts with `CSharp` contain C# versions of the samples; projects starting with `VisualBasic` contain Visual Basic versions.

The project structure is as follows:

| Project name | Description |
| --- | --- |
| `_Shared` | Contains shared code used by multiple samples, e.g. data classes and extensions |
| `_Shared.EF6` | The `LikeVisitor` sample uses EF6.  This project contains the `DbContext` and configuration classes |
| `CSharp`<br/>`VisualBasic` | Short samples, each of which is encapsulated in a single method |
| `CSharp_SetColumn`<br/>`VisualBasic_SetColumn` | <ul><li>Implements declaratively setting columns on a WPF DataGrid in code, using expression trees</li><li>`ParseFields` -- Parses the intended columns from an expression returning an array, anonymous type, or single property</li><li>`XamlBindingPathVisitor` -- Expression visitor that generates an object path usable with WPF databinding</li></ul> |
| `CSharp_LikeVisitor`<br/>`VisualBasic_LikeVisitor` | Uses an expression visitor to rewrite Visual Basic's `Like` operator usages to EF's `DbFunctions.Like` method.<br/>Note that while the visitor is implemented in both C# and Visual Basic, the calling code is only in Visual Basic, as C# doesn't have the `Like` operator |

## TODO

* DLR CallSite
