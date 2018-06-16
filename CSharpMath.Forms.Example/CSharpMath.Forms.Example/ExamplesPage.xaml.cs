﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CSharpMath.Forms.Example {
  public partial class ExamplesPage : ContentPage {
    public const string Numbers = @"0123456789";
    public const string Alphabets = @"abcdefghijklmnopqrstuvwxyz";
    public const string Capitals = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string Color = @"\color{#008}a\color{#00F}b\color{#080}c\color{#088}d\color{#08F}e\color{#0F0}f\color{#0F8}g\color{#0FF}h\color{#800}i\color{#808}j\color{#80F}k\color{#880}l\color{#888}m\color{#88F}n\color{#8F0}o\color{#8F8}p\color{#8FF}q\color{#F00}r\color{#F08}s\color{#F0F}t\color{#F80}u\color{#F88}v\color{#F8F}w\color{#FF0}x\color{#FF8}y\color{#FFF}z";
    //START mirror CSharpMath.Ios.Example
    public const string Radical = @"\sqrt{3}";
    public const string RadicalSum = @"2 + \sqrt{3}";
    public const string Fraction = @"\frac{2}{34}";
    public const string RadicalFraction = @"2+ \frac{\sqrt{3}}{2}";
    public const string IntPlusFraction = @"1+\frac23";
    public const string Matrix = @"\begin{pmatrix}
                           a & b\\ c & d
                            \end{pmatrix}
                            \begin{pmatrix}
                            \alpha & \beta \\ \gamma & \delta
                            \end{pmatrix} = 
                            \begin{pmatrix}
                            a\alpha + b\gamma & a\beta + b \delta \\
                            c\alpha + d\gamma & c\beta + d \delta 
                            \end{pmatrix}";
    public const string ShortMatrix = @"\begin{pmatrix} a & b\\ c & d \end{pmatrix}";
    public const string VeryShortMatrix = @"\begin{pmatrix}2\end{pmatrix}";
    public const string EmptyMatrix = @"\begin{pmatrix}\end{pmatrix}";
    public const string LeftRight = @"\left(\frac23\right)";
    public const string LeftRightMinus = @"\left(\frac23\right)-";
    public const string LeftSide = @"\frac{1}{\left(\sqrt{\phi \sqrt{5}}-\phi\right) e^{\frac25 \pi}}";
    public const string RightSide = @"1+\frac{e^{-2\pi}} {1 +\frac{e^{-4\pi}} {1+\frac{e^{-6\pi}} {1+\frac{e^{-8\pi}} {1+\cdots} } } }";
    public const string DeeplyNestedFraction = @"\frac{1}{\left(\sqrt{\phi \sqrt{5}}-\phi\right) e^{\frac25 \pi}} = 1+\frac{e^{-2\pi}} {1 +\frac{e^{-4\pi}} {1+\frac{e^{-6\pi}} {1+\frac{e^{-8\pi}} {1+\cdots} } } }";
    public const string NestedFraction = @"\frac{e^{-6\pi}} {1+\frac{e^{-8\pi}} {1+\cdots} }";
    public const string Exponential = @"e^2";
    public const string ExponentWithFraction = @"e^{4\frac25}";
    public const string ExponentWithProduct = @"e^{2x}";
    public const string ExponentWithPi = @"e^{2\pi}";
    public const string FractionWithRoot = @"\frac{1}{\sqrt{2}}";
    public const string NestedRadical = @"\sqrt{\sqrt{x}}";
    public const string NestedRadicalDeep = @"\sqrt{\sqrt{\sqrt{\sqrt{\sqrt{\sqrt{\sqrt{\sqrt{x}}}}}}}}";
    public const string Pi = @"\pi";
    public const string Phi = @"\phi";

    public const string QuadraticFormula = @"x = -b \pm \frac{\sqrt{b^2-4ac}}{2a}";
    public const string SomeLimit = @"\lim_{x\to\infty}\frac{e^2}{1-x}=\limsup_{\sigma}5";
    public const string SimpleLimit = @"\lim_{x\to\infty}3=3";
    public const string SomeIntegral = @"\int_{0}^{\infty}e^x \,dx=\oint_0^{\Delta}5\Gamma";
    public const string ShortIntegral = @"\int_0^1";
    public const string Commands = @"5\times(-2 \div 1) = -10";
    public const string SummationWithCup = @"\sum_{n=1}^{\infty}\frac{1+n}{1-n}=\bigcup_{1}C\cup B";
    public const string DoubleSummation = @"\sum \sum";
    public const string SummationBigCup = @"234 \bigcup_1";
    public const string SummationWithLimits = @"\sum_{n=1}^{\infty}";
    public const string Taylor = @"\begin{eqnarray} e^x  &=&  \sum_{x=0}^{\infty}\frac{x^n}{n!} \\ \\ \sin(x) &=& \sum_{x=0}^{\infty}(-1)^n\frac{(2x+1)^n}{(2n)!}  \\ \\ -\ln(1-x)   &=& \sum_{x=0}^{\infty}\frac{x^n}{n}  \ \ \ \ \ (-1 <= x < 1) \end{eqnarray}";
    public const string TwoSin = @"2 \sin";
    //END mirror CSharpMath.Ios.Example

    public const string BMartix = @"\begin{bmatrix} x_{11}&x_{12}&x_{13}&.&.&.&.&x_{1n} \end{bmatrix}";

    public ExamplesPage() => InitializeComponent();

    public static IEnumerable<FieldInfo> AllConstants { get; } = typeof(ExamplesPage).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).
      Where(fi => fi.IsLiteral && !fi.IsInitOnly);
    static readonly string latex = string.Join(@"\\", AllConstants.Select(info => $@"{info.Name}: {info.GetRawConstantValue()}"));

    protected override void OnAppearing() {
      base.OnAppearing();
      App.AllViews.Add(View);
      View.FontSize = 50;
      View.LaTeX = latex;
      View.InvalidateSurface();
    }
    protected override void OnDisappearing() {
      //App.AllViews.Remove(View);
      base.OnDisappearing();
    }
  }
}
