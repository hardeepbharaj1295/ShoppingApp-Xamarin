package md5e3d4171976f732c9989d6a5027f5c18a;


public class ViewHolders
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("App1.Resources.Model.ViewHolders, App1", ViewHolders.class, __md_methods);
	}


	public ViewHolders ()
	{
		super ();
		if (getClass () == ViewHolders.class)
			mono.android.TypeManager.Activate ("App1.Resources.Model.ViewHolders, App1", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
