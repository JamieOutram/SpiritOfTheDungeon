using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMeta
{
    public int Size { get { return members.Count; } }
    public List<PartyMemberMeta> members { get; private set; }


    public void AddMember(PartyMemberMeta newMember)
    {
        if (!members.Contains(newMember))
            members.Add(newMember);
        else
            Debug.LogError("Attempted to add duplicate member");
    }

    public void RemoveMember(PartyMemberMeta oldMember)
    {
        if (members.Contains(oldMember))
            members.Remove(oldMember);
        else
            Debug.LogError("Attempted to remove member which is not in the party");
    }

    public void ClearParty()
    {
        members.Clear(); //This Could cause a memory leak, hope trash manager is smart enough
    }
}
