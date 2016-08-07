﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Wrox.ProCSharp.Collections
{
  public class PriorityDocumentManager
  {
    private readonly LinkedList<Document> documentList;

    // priorities 0.9
    private readonly List<LinkedListNode<Document>> priorityNodes;

    public PriorityDocumentManager()
    {
      documentList = new LinkedList<Document>();

      priorityNodes = new List<LinkedListNode<Document>>(10);
      for (int i = 0; i < 10; i++)
      {
        priorityNodes.Add(new LinkedListNode<Document>(null));
      }
    }

    public void AddDocument(Document d)
    {
      Contract.Requires<ArgumentNullException>(d != null, "argument d must not be null");
      //  if (d == null) throw new ArgumentNullException("d");

      AddDocumentToPriorityNode(d, d.Priority);
    }

    private void AddDocumentToPriorityNode(Document doc, int priority)
    {
      Contract.Requires<ArgumentException>(priority >= 0 && priority < 10, "priority value must be between 0 and 9");
      //if (priority > 9 || priority < 0)
      //    throw new ArgumentException("Priority must be between 0 and 9");

      if (priorityNodes[priority].Value == null)
      {
        --priority;
        if (priority >= 0)
        {
          // check for the next lower priority
          AddDocumentToPriorityNode(doc, priority);
        }
        else // now no priority node exists with the same priority or lower
        // add the new document to the end
        {
          documentList.AddLast(doc);
          priorityNodes[doc.Priority] = documentList.Last;
        }
        return;
      }
      else // a priority node exists
      {
        LinkedListNode<Document> prioNode = priorityNodes[priority];
        if (priority == doc.Priority)
        // priority node with the same priority exists
        {
          documentList.AddAfter(prioNode, doc);

          // set the priority node to the last document with the same priority
          priorityNodes[doc.Priority] = prioNode.Next;
        }
        else // only priority node with a lower priority exists
        {
          // get the first node of the lower priority
          LinkedListNode<Document> firstPrioNode = prioNode;

          while (firstPrioNode.Previous != null &&
             firstPrioNode.Previous.Value.Priority == prioNode.Value.Priority)
          {
            firstPrioNode = prioNode.Previous;
            prioNode = firstPrioNode;
          }

          documentList.AddBefore(firstPrioNode, doc);

          // set the priority node to the new value
          priorityNodes[doc.Priority] = firstPrioNode.Previous;
        }
      }
    }

    public void DisplayAllNodes()
    {
      foreach (Document doc in documentList)
      {
        Console.WriteLine("priority: {0}, title {1}", doc.Priority, doc.Title);
      }
    }

    // returns the document with the highest priority
    // (that's first in the linked list)
    public Document GetDocument()
    {
      Document doc = documentList.First.Value;
      documentList.RemoveFirst();
      return doc;
    }

  }

}
