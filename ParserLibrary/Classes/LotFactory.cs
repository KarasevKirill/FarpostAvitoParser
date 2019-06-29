using ParserLibrary.Interfaces;
using System;

namespace ParserLibrary.Classes
{
    public class LotFactory<LotType> : ILotFactory where LotType : ILot, new()
    {
        public ILot GetNewLot()
        {
            return new LotType();
        }
    }
}
