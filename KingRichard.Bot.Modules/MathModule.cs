using Discord.Commands;
using System;
using System.Threading.Tasks;
using KingRichard.Calculator;
using QUT;
using KingRichard.Bot.Modules.Attributes;

namespace KingRichard.Bot.Modules
{
    [Module(CommandsConstants.MathModuleName)]
    [Description(CommandsConstants.MathModuleDescription)]
    public class MathModule : ExtendDefaultModule
    {
        #region Commands

        [Command(CommandsConstants.CalculateCommandName), Description(CommandsConstants.CalculateCommandDescription)]
        [Alias(CommandsConstants.CalculateCommandAlias)]
        [Parameter(CommandsConstants.Expression, ParameterType.Unparsed)]
        public async Task Calculate(CommandEventArgs e)
        {
            string expr = e.GetArg(CommandsConstants.Expression);

            try
            {      
                double result = RichardCalculator.Calculate(expr);

                await SendStyleMessage(e.Channel, String.Format(SettingsModuleResource.ExpressionResult, expr, result), SettingsModuleResource.Markdown);
            }
            catch (LexicalException exp)
            {
                await SendStyleMessage(e.Channel, String.Format(SettingsModuleResource.LexicalException, expr, exp.Message), SettingsModuleResource.Markdown);
            }
            catch (SyntaxException exp)
            {
                await SendStyleMessage(e.Channel, String.Format(SettingsModuleResource.SyntaxException, expr, exp.Message), SettingsModuleResource.Markdown);
            }
            catch (Exception exp)
            {
                await SendStyleMessage(e.Channel, String.Format(SettingsModuleResource.Exception, expr, exp.Message), SettingsModuleResource.Markdown);
            }
        }

        #endregion
    }
}
