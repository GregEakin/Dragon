// Copyright 2024 Gregory Eakin
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// This is a C# front-end parser derived from the Dragon book, found in Appendix A.
// Aho, Alfred V., and Alfred V. Aho. Compilers: Principles, Techniques, & Tools. Boston: Pearson / Addison Wesley, 2007. Print.

namespace Dragon.Lexical;

public class Tag
{
    public const int
        AND = 256, BASIC = 257, BREAK = 258, DO = 259, ELSE = 260,
        EQ = 261, FALSE = 262, GE = 263, ID = 264, IF = 265,
        INDEX = 266, LE = 267, MINUS = 268, NE = 269, NUM = 270,
        OR = 271, REAL = 272, TEMP = 273, TRUE = 274, WHILE = 275,
        NOT = 276, THEN = 277, ELSEIF = 278, DER = 279, LOOP = 280,
        FOR = 281, IN = 282, ELSEWHEN = 283, RETURN = 284, PLUS = 281;
}