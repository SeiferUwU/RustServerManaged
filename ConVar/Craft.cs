using UnityEngine;

namespace ConVar;

[Factory("craft")]
public class Craft : ConsoleSystem
{
	[ServerVar]
	public static bool instant;

	[ServerUserVar]
	public static void add(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if (!basePlayer || basePlayer.IsDead())
		{
			return;
		}
		int num = args.GetInt(0);
		int num2 = args.GetInt(1, 1);
		int num3 = (int)args.GetUInt64(2, 0uL);
		bool flag = args.GetBool(3);
		if (num2 < 1)
		{
			return;
		}
		ItemDefinition itemDefinition = ItemManager.FindItemDefinition(num);
		if (itemDefinition == null)
		{
			args.ReplyWith("Item not found");
			return;
		}
		ItemBlueprint itemBlueprint = ItemManager.FindBlueprint(itemDefinition);
		if (!itemBlueprint)
		{
			args.ReplyWith("Blueprint not found");
			return;
		}
		if (!itemBlueprint.userCraftable)
		{
			args.ReplyWith("Item is not craftable");
			return;
		}
		if (!basePlayer.blueprints.CanCraft(num, num3, basePlayer.userID))
		{
			num3 = 0;
			if (!basePlayer.blueprints.CanCraft(num, num3, basePlayer.userID))
			{
				args.ReplyWith("You can't craft this item");
				return;
			}
			args.ReplyWith("You don't have permission to use this skin, so crafting unskinned");
		}
		int num4 = num2;
		int num5 = num2;
		if (flag)
		{
			num4 = Mathf.Min(num2, 5);
			num5 = 1;
		}
		for (int num6 = num4; num6 >= num5; num6--)
		{
			if (basePlayer.inventory.crafting.CraftItem(itemBlueprint, basePlayer, null, num6, num3))
			{
				return;
			}
		}
		args.ReplyWith("Couldn't craft!");
	}

	[ServerUserVar]
	public static void canceltask(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer && !basePlayer.IsDead())
		{
			int iID = args.GetInt(0);
			if (!basePlayer.inventory.crafting.CancelTask(iID))
			{
				args.ReplyWith("Couldn't cancel task!");
			}
		}
	}

	[ServerUserVar]
	public static void cancel(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer && !basePlayer.IsDead())
		{
			int itemid = args.GetInt(0);
			basePlayer.inventory.crafting.CancelBlueprint(itemid);
		}
	}

	[ServerUserVar]
	public static void fasttracktask(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer && !basePlayer.IsDead())
		{
			int taskID = args.GetInt(0);
			if (!basePlayer.inventory.crafting.FastTrackTask(taskID))
			{
				args.ReplyWith("Couldn't fast track task!");
			}
		}
	}
}
