using Facepunch;
using ProtoBuf;
using UnityEngine;

public class ElectricGenerator : IOEntity
{
	public float electricAmount = 8f;

	public override bool IsRootEntity()
	{
		return true;
	}

	public override int MaximalPowerOutput()
	{
		return Mathf.FloorToInt(electricAmount);
	}

	public override int ConsumptionAmount()
	{
		return 0;
	}

	public override int GetCurrentEnergy()
	{
		return (int)electricAmount;
	}

	public override int GetPassthroughAmount(int outputSlot = 0)
	{
		return GetCurrentEnergy();
	}

	public override void UpdateOutputs()
	{
		currentEnergy = GetCurrentEnergy();
		IOSlot[] array = outputs;
		foreach (IOSlot iOSlot in array)
		{
			if (iOSlot.connectedTo.Get() != null)
			{
				iOSlot.connectedTo.Get().UpdateFromInput(currentEnergy, iOSlot.connectedToSlot);
			}
		}
	}

	public override void IOStateChanged(int inputAmount, int inputSlot)
	{
		base.IOStateChanged(inputAmount, inputSlot);
	}

	public override void PostServerLoad()
	{
		base.PostServerLoad();
		Invoke(ForcePuzzleReset, 4f);
	}

	private void ForcePuzzleReset()
	{
		PuzzleReset component = GetComponent<PuzzleReset>();
		if (component != null)
		{
			component.DoReset();
			component.ResetTimer();
		}
	}

	public override void Save(SaveInfo info)
	{
		base.Save(info);
		if (!info.forDisk)
		{
			return;
		}
		PuzzleReset component = GetComponent<PuzzleReset>();
		if ((bool)component)
		{
			info.msg.puzzleReset = Pool.Get<ProtoBuf.PuzzleReset>();
			info.msg.puzzleReset.playerBlocksReset = component.playersBlockReset;
			if (component.playerDetectionOrigin != null)
			{
				info.msg.puzzleReset.playerDetectionOrigin = component.playerDetectionOrigin.position;
			}
			info.msg.puzzleReset.playerDetectionRadius = component.playerDetectionRadius;
			info.msg.puzzleReset.scaleWithServerPopulation = component.scaleWithServerPopulation;
			info.msg.puzzleReset.timeBetweenResets = component.timeBetweenResets;
			info.msg.puzzleReset.checkSleepingAIZForPlayers = component.CheckSleepingAIZForPlayers;
			info.msg.puzzleReset.ignoreAboveGroundPlayers = component.ignoreAboveGroundPlayers;
			info.msg.puzzleReset.broadcastResetMessage = component.broadcastResetMessage;
			info.msg.puzzleReset.resetPhrase = component.resetPhrase?.token ?? "";
			info.msg.puzzleReset.radiationReset = component.radiationReset;
			info.msg.puzzleReset.pauseUntilLooted = component.pauseUntilLooted;
		}
	}

	public override void Load(LoadInfo info)
	{
		base.Load(info);
		if (!info.fromDisk || info.msg.puzzleReset == null)
		{
			return;
		}
		PuzzleReset component = GetComponent<PuzzleReset>();
		if (!(component != null))
		{
			return;
		}
		component.playersBlockReset = info.msg.puzzleReset.playerBlocksReset;
		if (component.playerDetectionOrigin != null)
		{
			component.playerDetectionOrigin.position = info.msg.puzzleReset.playerDetectionOrigin;
		}
		component.playerDetectionRadius = info.msg.puzzleReset.playerDetectionRadius;
		component.scaleWithServerPopulation = info.msg.puzzleReset.scaleWithServerPopulation;
		component.timeBetweenResets = info.msg.puzzleReset.timeBetweenResets;
		component.CheckSleepingAIZForPlayers = info.msg.puzzleReset.checkSleepingAIZForPlayers;
		component.ignoreAboveGroundPlayers = info.msg.puzzleReset.ignoreAboveGroundPlayers;
		component.broadcastResetMessage = info.msg.puzzleReset.broadcastResetMessage;
		if (!string.IsNullOrEmpty(info.msg.puzzleReset.resetPhrase))
		{
			Translate.Phrase phrase = Translate.GetPhrase(info.msg.puzzleReset.resetPhrase);
			if (phrase != null)
			{
				component.resetPhrase = phrase;
			}
		}
		component.radiationReset = info.msg.puzzleReset.radiationReset;
		component.pauseUntilLooted = info.msg.puzzleReset.pauseUntilLooted;
		component.ResetTimer();
	}
}
