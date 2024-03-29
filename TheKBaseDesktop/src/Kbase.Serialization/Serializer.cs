/*
This file is part of TheKBase Desktop
A Multi-Hierarchical  Information Manager
Copyright (C) 2004-2006 Daniel Rosenstark

TheKBase Desktop is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
("GPL") version 2 as published by the Free Software Foundation.
See the file LICENSE.TXT for the full text of the GNU GPL, or see
http://www.gnu.org/licenses/gpl.txt

For using TheKBase Desktop with software that can not be combined with 
the GNU GPL or any other queries, please contact Daniel Rosenstark 
(license@thekbase.com).
*/
using System;
using System.Xml;

namespace Kbase.Serialization
{
	/// <summary>
	/// Abstract class, subclasses override the Save and Restore 
    /// 
    /// Serializer is used to serialize a SerializableUniverse, 
    /// though you can instantiate its subclasses directly.
    /// I admit, with only one subclass this hardly makes sense, but
    /// I used to have a few serialization options.
	/// </summary>
	public abstract class Serializer
	{
		public Serializer()
		{
		}

		public void Save(SerializableUniverse serializablePiece, string path) 
		{
			System.IO.TextWriter writer = null;
			try 
			{
				writer = new System.IO.StreamWriter(path);
				Save(serializablePiece,writer);
			} 
			finally 
			{
				if (writer != null)
					writer.Close();
			}
		}

        public void Save(SerializableUniverse serializablePiece, System.IO.TextWriter writer)
        {
            XmlDocument doc = GetXml(serializablePiece);
            doc.Save(writer);
        }


        public abstract XmlDocument GetXml(SerializableUniverse serializablePiece);


		public abstract SerializableUniverse Restore(string path);
	
		public static Serializer GetDefaultSerializer() {
			return new SerializerDom();
		}
	}
}
