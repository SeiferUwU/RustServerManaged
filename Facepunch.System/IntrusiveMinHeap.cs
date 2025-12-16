public struct IntrusiveMinHeap<T> where T : IMinHeapNode<T>
{
	private T head;

	public bool Empty => head == null;

	public void Add(T item)
	{
		if (head == null)
		{
			head = item;
			return;
		}
		if (head.child == null && item.order <= head.order)
		{
			T child = head;
			item.child = child;
			head = item;
			return;
		}
		T child2 = head;
		while (child2.child != null && child2.child.order < item.order)
		{
			child2 = child2.child;
		}
		T child3 = child2.child;
		item.child = child3;
		T child4 = item;
		child2.child = child4;
	}

	public T Pop()
	{
		T result = head;
		head = head.child;
		result.child = default(T);
		return result;
	}
}
