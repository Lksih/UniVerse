using System;
using System.Collections.Generic;
using System.Text;

namespace UniVerse
{
    public interface IMessages
    {
        void VerseAddedMessage();

        void NullVerseFieldsError(string name, string author, string text);

        void TestDoneMessage();
    }
}
