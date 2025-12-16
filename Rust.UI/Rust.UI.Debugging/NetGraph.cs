using System.Collections.Generic;
using System.Linq;
using Network;
using UnityEngine;

namespace Rust.UI.Debugging;

public class NetGraph : SingletonComponent<NetGraph>
{
	public Canvas canvas;

	public CanvasGroup group;

	public GameObject rootPanel;

	private NetGraphRow[] rows;

	public bool Enabled
	{
		set
		{
			group.alpha = (value ? 1 : 0);
			canvas.enabled = value;
			rootPanel.SetActive(value);
		}
	}

	public void Start()
	{
		rows = GetComponentsInChildren<NetGraphRow>(includeInactive: true);
	}

	public void UpdateFrom(Stats incomingStats)
	{
		for (int i = 0; i < rows.Length; i++)
		{
			rows[i].Hide();
		}
		int num = 0;
		foreach (KeyValuePair<string, Stats.Node> item in incomingStats.Previous.Children.OrderByDescending((KeyValuePair<string, Stats.Node> y) => y.Value.Bytes))
		{
			if (num >= rows.Length)
			{
				break;
			}
			rows[num].UpdateFrom(item);
			num++;
		}
	}
}
