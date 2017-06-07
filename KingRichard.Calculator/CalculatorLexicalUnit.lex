%namespace CalculatorLexicalScanner
%using QUT;
%using QUT.Gppg;
%using System.Linq;
%using CalculatorSyntaxScanner;

Numeral [0-9]
Int {Numeral}+
Double {Int}\.{Int}
Comma \,
Plus \+
Minus \-
Mult \*
Divide \/
Pow \^
Sqrt sqrt
Log log
Ln ln
Pi pi
Exp exp
Sin sin
Cos cos
Tangent tg
Cotangent ctg
Exclamations \!
ModuleBracket \|
LeftBracketS \[
RightBracketS \]
LeftBracket \(
RightBracket \)


%%
{Int} {
  yylval.sVal = yytext.ToUpper();
  return (int)Tokens.Int;
}

{Double} {
  yylval.sVal = yytext.ToUpper();
  return (int)Tokens.Double;
}

{Comma} {
  return (int)Tokens.Comma;
}

{Plus} {
  return (int)Tokens.Plus;
}

{Minus} {
  return (int)Tokens.Minus;
}

{Mult} {
  return (int)Tokens.Mult;
}

{Divide} {
  return (int)Tokens.Divide;
}

{Pow} {
  return (int)Tokens.Pow;
}

{Sqrt} {
  return (int)Tokens.Sqrt;
}

{Log} {
  return (int)Tokens.Log;
}

{Ln} {
  return (int)Tokens.Ln;
}

{Pi} {
  return (int)Tokens.Pi;
}

{Exp} {
  return (int)Tokens.Exp;
}

{Sin} {
  return (int)Tokens.Sin;
}

{Cos} {
  return (int)Tokens.Cos;
}

{Tangent} {
  return (int)Tokens.Tangent;
}

{Cotangent} {
  return (int)Tokens.Cotangent;
}

{Exclamations} {
  return (int)Tokens.Exclamations;
}

{ModuleBracket} {
  return (int)Tokens.ModuleBracket;
}

{LeftBracketS} {
  return (int)Tokens.LeftBracketS;
}

{RightBracketS} {
  return (int)Tokens.RightBracketS;
}

{LeftBracket} {
  return (int)Tokens.LeftBracket;
}

{RightBracket} {
  return (int)Tokens.RightBracket;
}

[^ \r\n] {
  LexError();
  return (int)Tokens.EOF;
}

%{
  yylloc = new LexLocation(tokLin, tokCol, tokELin, tokECol);
%}

%%

public override void yyerror(string format, params object[] args)
{
  var ww = args.Skip(1).Cast<string>().ToArray();
  string errorMsg = string.Format("({0},{1}): Detect {2},  expected {3}", yyline, yycol, args[0], string.Join(" or ", ww));
  throw new SyntaxException(errorMsg);
}

public void LexError()
{
  string errorMsg = string.Format("({0},{1}): Unknown character {2}", yyline, yycol, yytext);
    throw new LexicalException(errorMsg);
}