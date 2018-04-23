using System;
using System.Collections.Generic;

namespace Constellation.CoreNodes {
    public class ConditionParser {
        private List<ICondition> conditions;
        private Variable thenVar;
        private Variable elseVar;

        public ConditionParser (string _condition, string _then, string _else, Variable _var1, Variable _var2, Variable _var3) {
            conditions = new List<ICondition> ();
            string condition = _condition;
            string[] equal = condition.Split (new string[] { "==" }, StringSplitOptions.None);

            LinkThenVar (_then, _var1, _var2, _var3);
            LinkElseVar (_else, _var1, _var2, _var3);

            if (equal.Length == 2) {
                LinkEqualVar (equal, _var1, _var2, _var3);
                return;
            }

            string[] notEqual = condition.Split (new string[] { "!=" }, StringSplitOptions.None);
            if (notEqual.Length == 2) {
                LinkNotEqualVar (notEqual, _var1, _var2, _var3);
                return;
            }

            string[] lessEqual = condition.Split (new string[] { "<=" }, StringSplitOptions.None);
            if (lessEqual.Length == 2) {
                LinkLessEqualVar (lessEqual, _var1, _var2, _var3);
                return;
            }

            string[] moreEqual = condition.Split (new string[] { ">=" }, StringSplitOptions.None);
            if (moreEqual.Length == 2) {
                LinkMoreEqualVar (moreEqual, _var1, _var2, _var3);
                return;
            }

            string[] more = condition.Split (new string[] { ">" }, StringSplitOptions.None);
            if (more.Length == 2) {
                LinkMoreVar (more, _var1, _var2, _var3);
                return;
            }

            string[] less = condition.Split (new string[] { "<" }, StringSplitOptions.None);
            if (less.Length == 2) {
                LinkLessVar (less, _var1, _var2, _var3);
                return;
            }

        }

        private void LinkThenVar (string thenInstructions, Variable _var1, Variable _var2, Variable _var3) {
            if (thenInstructions == "$1")
                thenVar = _var1;
            else if (thenInstructions == "$2")
                thenVar = _var2;
            else if (thenInstructions == "$3")
                thenVar = _var3;
            else
                thenVar = new Variable ().Set (thenInstructions);
        }

        private void LinkElseVar (string elseInstructions, Variable _var1, Variable _var2, Variable _var3) {
            if (elseInstructions == "$1")
                elseVar = _var1;
            else if (elseInstructions == "$2")
                elseVar = _var2;
            else if (elseInstructions == "$3")
                elseVar = _var3;
            else
                elseVar = new Variable ().Set (elseInstructions);
        }

        private void LinkMoreVar (string[] condition, Variable _var1, Variable _var2, Variable _var3) {
            Variable var1 = new Variable ();
            if (condition[0] == "$1")
                var1 = _var1;
            else if (condition[0] == "$2")
                var1 = _var2;
            else if (condition[0] == "$3")
                var1 = _var3;
            else
                var1 = new Variable ().Set (condition[0]);

            Variable var2 = new Variable ();
            if (condition[1] == "$1")
                var2 = _var1;
            else if (condition[1] == "$2")
                var2 = _var2;
            else if (condition[1] == "$3")
                var2 = _var3;
            else
                var2 = new Variable ().Set (condition[1]);

            var newCondition = new MoreCondition (var1, var2);
            conditions.Add (newCondition as ICondition);
        }

        private void LinkMoreEqualVar (string[] condition, Variable _var1, Variable _var2, Variable _var3) {
            Variable var1 = new Variable ();
            if (condition[0] == "$1")
                var1 = _var1;
            else if (condition[0] == "$2")
                var1 = _var2;
            else if (condition[0] == "$3")
                var1 = _var3;
            else
                var1 = new Variable ().Set (condition[0]);

            Variable var2 = new Variable ();
            if (condition[1] == "$1")
                var2 = _var1;
            else if (condition[1] == "$2")
                var2 = _var2;
            else if (condition[1] == "$3")
                var2 = _var3;
            else
                var2 = new Variable ().Set (condition[1]);

            var newCondition = new MoreOrEqualCondition (var1, var2);
            conditions.Add (newCondition as ICondition);
        }

        private void LinkLessEqualVar (string[] condition, Variable _var1, Variable _var2, Variable _var3) {
            Variable var1 = new Variable ();
            if (condition[0] == "$1")
                var1 = _var1;
            else if (condition[0] == "$2")
                var1 = _var2;
            else if (condition[0] == "$3")
                var1 = _var3;
            else
                var1 = new Variable ().Set (condition[0]);

            Variable var2 = new Variable ();
            if (condition[1] == "$1")
                var2 = _var1;
            else if (condition[1] == "$2")
                var2 = _var2;
            else if (condition[1] == "$3")
                var2 = _var3;
            else
                var2 = new Variable ().Set (condition[1]);

            var newCondition = new LessOrEqualCondition (var1, var2);
            conditions.Add (newCondition as ICondition);
        }

        private void LinkLessVar (string[] condition, Variable _var1, Variable _var2, Variable _var3) {
            Variable var1 = new Variable ();
            if (condition[0] == "$1")
                var1 = _var1;
            else if (condition[0] == "$2")
                var1 = _var2;
            else if (condition[0] == "$3")
                var1 = _var3;
            else
                var1 = new Variable ().Set (condition[0]);

            Variable var2 = new Variable ();
            if (condition[1] == "$1")
                var2 = _var1;
            else if (condition[1] == "$2")
                var2 = _var2;
            else if (condition[1] == "$3")
                var2 = _var3;
            else
                var2 = new Variable ().Set (condition[1]);

            var newCondition = new LessCondition (var1, var2);
            conditions.Add (newCondition as ICondition);
        }

        private void LinkNotEqualVar (string[] condition, Variable _var1, Variable _var2, Variable _var3) {
            Variable var1 = new Variable ();
            if (condition[0] == "$1")
                var1 = _var1;
            else if (condition[0] == "$2")
                var1 = _var2;
            else if (condition[0] == "$3")
                var1 = _var3;
            else
                var1 = new Variable ().Set (condition[0]);

            Variable var2 = new Variable ();
            if (condition[1] == "$1")
                var2 = _var1;
            else if (condition[1] == "$2")
                var2 = _var2;
            else if (condition[1] == "$3")
                var2 = _var3;
            else
                var2 = new Variable ().Set (condition[1]);

            var newCondition = new NotEqualCondition (var1, var2);
            conditions.Add (newCondition as ICondition);
        }

        private void LinkEqualVar (string[] condition, Variable _var1, Variable _var2, Variable _var3) {
            Variable var1 = new Variable ();
            if (condition[0] == "$1")
                var1 = _var1;
            else if (condition[0] == "$2")
                var1 = _var2;
            else if (condition[0] == "$3")
                var1 = _var3;
            else
                var1 = new Variable ().Set (condition[0]);

            Variable var2 = new Variable ();
            if (condition[1] == "$1")
                var2 = _var1;
            else if (condition[1] == "$2")
                var2 = _var2;
            else if (condition[1] == "$3")
                var2 = _var3;
            else
                var2 = new Variable ().Set (condition[1]);

            var newCondition = new EqualCondition (var1, var2);
            conditions.Add (newCondition as ICondition);
        }

        public bool isConditionMet () {
            foreach (var condition in conditions) {
                if (condition.isConditionMet ())
                    return true;
            }
            return false;
        }

        public Variable ConditionResult () {
            foreach (var condition in conditions) {
                if (condition.isConditionMet ())
                    return thenVar;
            }
            return elseVar;
        }
    }
}