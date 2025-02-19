using System.Linq;
using NWN.Native.API;

namespace Anvil.API
{
  /// <summary>
  /// The race type of a creature. Contains additional ruleset information about the race.
  /// </summary>
  public sealed class NwRace
  {
    private readonly CNWRace raceInfo;

    internal NwRace(ushort raceId, CNWRace raceInfo)
    {
      Id = raceId;
      this.raceInfo = raceInfo;
    }

    /// <summary>
    /// Gets the amount of points granted to this race during ability score point buy in character creation.
    /// </summary>
    public int AbilityPointBuyAmount => raceInfo.m_nAbilitiesPointBuyNumber;

    /// <summary>
    /// Gets the default age used during character creation.
    /// </summary>
    public int DefaultAge => raceInfo.m_nAge;

    /// <summary>
    /// Gets the description used by default for the character's description.
    /// </summary>
    public string? DefaultCharacterDescription => raceInfo.GetDefaultBiographyText().ToString();

    /// <summary>
    /// Gets the description of this race.
    /// </summary>
    public string? Description => raceInfo.GetDescriptionText().ToString();

    /// <summary>
    /// Gets the number of extra feats that this race grants characters at first level.
    /// </summary>
    public int ExtraFeatsAtFirstLevel => raceInfo.m_nExtraFeatsAtFirstLevel;

    /// <summary>
    /// Gets the number of extra skill points that this race grants characters per level.
    /// </summary>
    public int ExtraSkillPointsPerLevel => raceInfo.m_nExtraSkillPointsPerLevel;

    /// <summary>
    /// Gets the favoured class for this race, for the purposes of multi-classing.
    /// </summary>
    public NwClass? FavoredClass => NwClass.FromClassId(raceInfo.m_nFavoredClass);

    /// <summary>
    /// Gets the initial muliplier for skill points given at first level.
    /// </summary>
    public int FirstLevelSkillPointsMultiplier => raceInfo.m_nFirstLevelSkillPointsMultiplier;

    /// <summary>
    /// Gets the id of this race.
    /// </summary>
    public ushort Id { get; }

    /// <summary>
    /// Gets if this race can be chosen/used by players.
    /// </summary>
    public bool IsPlayerRace => raceInfo.m_bIsPlayerRace.ToBool();

    /// <summary>
    /// Gets the name of this race.
    /// </summary>
    public string? Name => raceInfo.GetNameText().ToString();

    /// <summary>
    /// Gets the level period that feats are granted from this race.
    /// </summary>
    public int NormalFeatEveryNthLevel => raceInfo.m_nNormalFeatEveryNthLevel;

    /// <summary>
    /// Gets how many feats are given to the character every <see cref="NormalFeatEveryNthLevel"/>.
    /// </summary>
    public int NumberNormalFeatsEveryNthLevel => raceInfo.m_nNumberNormalFeatsEveryNthLevel;

    /// <summary>
    /// Gets the plural name of this race.
    /// </summary>
    public string? PluralName => raceInfo.GetNamePluralText().ToString();

    /// <summary>
    /// Gets the associated <see cref="Id"/> for this race.
    /// </summary>
    public RacialType RacialType => (RacialType)Id;

    /// <summary>
    /// Gets the ability score used to determine bonus skill points at level up.
    /// </summary>
    public Ability SkillPointModifierAbility => (Ability)raceInfo.m_nSkillPointModifierAbility;

    /// <summary>
    /// Creates a race structure from the specified race id.
    /// </summary>
    /// <param name="raceId">The associated race id.</param>
    /// <returns>The associated <see cref="NwRace"/> structure, or null if the race has no matching entry.</returns>
    public static NwRace? FromRaceId(ushort? raceId)
    {
      return raceId != IntegerExtensions.AsUShort(-1) ? FromRaceId((int?)raceId) : null;
    }

    /// <summary>
    /// Resolves a <see cref="NwRace"/> from a race id.
    /// </summary>
    /// <param name="raceId">The id of the race to resolve.</param>
    /// <returns>The associated <see cref="NwRace"/> instance. Null if the race id is invalid.</returns>
    public static NwRace? FromRaceId(int? raceId)
    {
      return raceId != null ? NwRuleset.Races.ElementAtOrDefault(raceId.Value) : null;
    }

    /// <summary>
    /// Resolves a <see cref="NwRace"/> from a <see cref="Anvil.API.RacialType"/>.
    /// </summary>
    /// <param name="racialType">The racial type to resolve.</param>
    /// <returns>The associated <see cref="NwRace"/> instance. Null if the racial type is invalid.</returns>
    public static NwRace? FromRacialType(RacialType racialType)
    {
      return NwRuleset.Races.ElementAtOrDefault((int)racialType);
    }

    public static implicit operator NwRace?(RacialType racialType)
    {
      return NwRuleset.Races.ElementAtOrDefault((int)racialType);
    }

    /// <summary>
    /// Gets the ability score adjustment for the specified ability.
    /// </summary>
    /// <param name="ability">The ability to query.</param>
    /// <returns>A signed byte of the adjustment to the creature's ability score.</returns>
    public sbyte GetAbilityAdjustment(Ability ability)
    {
      return raceInfo.GetAbilityAdjust((byte)ability).AsSByte();
    }

    /// <summary>
    /// Gets if the specified feat is one that is automatically granted by this race at first level.
    /// </summary>
    /// <param name="feat">The feat to query.</param>
    /// <returns>True if this is a default granted feat for this race, otherwise false.</returns>
    public bool IsFirstLevelGrantedFeat(NwFeat feat)
    {
      return raceInfo.IsFirstLevelGrantedFeat(feat.Id).ToBool();
    }

    /// <summary>
    /// Gets that race favored enemy feat.
    /// </summary>
    /// <returns>The default favored enemy feat for that race, otherwise null.</returns>
    public NwFeat? GetFavoredEnemyFeat()
    {
      return NwFeat.FromFeatId(raceInfo.m_nFavoredEnemyFeat);
    }
  }
}
