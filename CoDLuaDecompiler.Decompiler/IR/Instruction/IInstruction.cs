using System.Collections.Generic;
using CoDLuaDecompiler.Decompiler.CFG;
using CoDLuaDecompiler.Decompiler.IR.Expression;
using CoDLuaDecompiler.Decompiler.IR.Identifiers;

namespace CoDLuaDecompiler.Decompiler.IR.Instruction
{
    /// <summary>
    /// A single instruction or statement, initially translated from a Lua opcode, but can be simplified into more powerful "instructions"
    /// </summary>
    public abstract class IInstruction
    {
        /// <summary>
        /// The original lua bytecode op within the function that generated this instruction
        /// </summary>
        public int OpLocation { get; set; }

        /// <summary>
        /// The instruction index in a basic block before propogation is done
        /// </summary>
        public int PrePropagationIndex { get; set; } = 0;
        /// <summary>
        /// Backpointer to the containing block. Used for some analysis
        /// </summary>
        public BasicBlock Block { get; set; }
        
        public virtual void Parenthesize() { }
        
        /// <summary>
        /// Gets all the identifiers that are defined by this instruction
        /// </summary>
        /// <returns></returns>
        public virtual HashSet<Identifier> GetDefines(bool regOnly)
        {
            return new HashSet<Identifier>();
        }
        
        /// <summary>
        /// Gets all the identifiers that are used (but not defined) by this instruction
        /// </summary>
        /// <returns></returns>
        public virtual HashSet<Identifier> GetUses(bool regOnly)
        {
            return new HashSet<Identifier>();
        }
        
        public virtual List<IExpression> GetExpressions()
        {
            return new List<IExpression>();
        }
        
        public virtual void RenameDefines(Identifier orig, Identifier newId) { }

        public virtual void RenameUses(Identifier orig, Identifier newId) { }

        public virtual bool ReplaceUses(Identifier orig, IExpression sub) { return false; }

        public virtual string WriteLua(int indentLevel)
        {
            return ToString();
        }
    }
}