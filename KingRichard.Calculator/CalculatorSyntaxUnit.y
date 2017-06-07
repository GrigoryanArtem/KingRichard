%output=CalculatorSyntaxUnit.cs 

%{
	public Parser(CalculatorLexicalScanner.Scanner scanner) : base(scanner) { }
	public double Result{ get; private set; }
%}

%union { 
	public string sVal;
	public double dVal;
}

%using System.IO
%using KingRichard.Calculator;

%namespace CalculatorSyntaxScanner

%start main_expression

%token <sVal> Int Double
%token Plus Minus Mult Divide Pow Sqrt Log Ln Pi Exp Sin Cos Tangent Cotangent Exclamations ModuleBracket LeftBracketS RightBracketS LeftBracket RightBracket Comma
%type <dVal> expression md_expression pow_expression f_expression uf_expression df_expression u_expression atom

%%

main_expression : expression { 
					Result = $1;
				}
				;

expression	    : md_expression { 
					$$ = $1;
				}
			    | expression Plus md_expression {
					$$ = $1 + $3;
				}
				| expression Minus md_expression {
					$$ = $1 - $3;
				}
			    ;

md_expression   : pow_expression  { 
					$$ = $1;
				}
			    | md_expression Mult pow_expression {
					$$ = $1 * $3;
				}
				| md_expression Divide pow_expression {
					$$ = $1 / $3;
				}
			    ;

pow_expression  : f_expression  { 
					$$ = $1;
				}
			    | pow_expression Pow f_expression {
					$$ = Math.Pow($1, $3);
				}
			    ;

f_expression    : u_expression {
					$$ = $1;
				}
				| uf_expression {
					$$ = $1;		
				}
				| df_expression {
					$$ = $1;		
				}
				;

uf_expression   : Sqrt u_expression {
					$$ = Math.Sqrt($2);
				}
				| Ln u_expression {
					$$ = Math.Log($2);
				}
				| Sin u_expression {
					$$ = Math.Sin($2);
				}
				| Cos u_expression {
					$$ = Math.Cos($2);
				}
				| Tangent u_expression {
					$$ = Math.Tan($2);
				}
				| Cotangent u_expression {
					$$ = 1 / Math.Tan($2);
				}
				| ModuleBracket u_expression ModuleBracket {
					$$ = Math.Abs($2);
				}
				| u_expression Exclamations {
					$$ = RichardMath.Factorial($1);
				}
				;

df_expression   : Log u_expression u_expression {
					$$ = Math.Log($3) / Math.Log($2); 
				}
				;

u_expression    : atom  { 
					$$ = $1;
				}
			    | Plus atom {
					$$ =  $2;
				}
				| Minus atom {
					$$ = -$2;
				}
			    ;
		
atom            : Double  { 
					$$ = Convert.ToDouble($1, CultureInfo.InvariantCulture);
				}
				| Int { 
					$$ = Convert.ToDouble($1, CultureInfo.InvariantCulture);
				}
				| Pi {
					$$ = Math.PI;
				}
				| Exp {
					$$ = Math.E;
				}
			    | LeftBracket expression RightBracket {
					$$ =  $2;
				}
				| LeftBracketS expression RightBracketS {
					$$ =  $2;
				}
				;
%%