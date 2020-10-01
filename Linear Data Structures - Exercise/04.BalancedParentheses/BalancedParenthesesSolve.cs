namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            var stack = new Stack<char>();
            stack.Push(parentheses[0]);
            for (int i = 1; i < parentheses.Length; i++)
            {
                char symbol = parentheses[i];
                switch (symbol)
                {
                    case '[':
                    case '{':
                    case '(':
                        stack.Push(symbol);
                        break;
                    case '}':
                        if (stack.Count>0 && stack.Peek() == '{')
                        {
                            stack.Pop();
                        }
                        else
                        {
                            stack.Push(symbol);
                        }
                        break;
                    case ']':
                        if (stack.Count > 0 && stack.Peek() == '[')
                        {
                            stack.Pop();
                        }
                        else
                        {
                            stack.Push(symbol);
                        }
                        break;
                    case ')':
                        if (stack.Count > 0 && stack.Peek() == '(')
                        {
                            stack.Pop();
                        }
                        else
                        {
                            stack.Push(symbol);
                        }
                        break;
                    default:
                        stack.Push(symbol);
                        break;
                }
            }

            return stack.Count == 0;
        }
    }
}
