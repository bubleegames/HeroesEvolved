using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

using JT.FWW;
using JT.FWW.Tools;
using BlGame.GameData;
using JT.FWW.GameData;
using GameDefine;
using BlGame.GameEntity;
using BlGame.FSM;
using BlGame.GuideDate;
using BlGame.Effect;
using BlGame;
using BlGame.GameState;
using BlGame.Ctrl;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ProtoBuf;
using GCToCS;
using GCToBS;
using BSToGC;
using System.IO;
using BlGame.Network;
using BlGame.Resource;
using BlGame.Model;
using BlGame.View;




public partial class CGLCtrl_GameLogic : UnitySingleton<CGLCtrl_GameLogic>
{
    const int PROTO_DESERIALIZE_ERROR = -1;

    public void HandleNetMsg(System.IO.Stream stream, int n32ProtocalID)
    {
        //Debug.Log("n32ProtocalID   " + (GSToGC.MsgID)n32ProtocalID);
        switch (n32ProtocalID)
        {
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_GCAskPingRet:
                OnNetMsg_NotifyPing(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyUserBaseInfo:
                OnNetMsg_NotifyUserBaseInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_GCAskRet:
                OnNetMsg_NotifyReturn(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyNetClash:
                OnNetMsg_NotifyNetClash(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBattleBaseInfo:
                OnNetMsg_NotifyBattleBaseInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBattleSeatPosInfo:
                OnNetMsg_NotifyBattleSeatPosInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBattleStateChange:
                OnNetMsg_NotifyBattleStateChange(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyCurBattleChange:
                OnNetMsg_NotifyCurBattleChange(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectAppear:
                OnNetMsg_NotifyGameObjectAppear(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectDisappear:
                OnNetMsg_NotifyGameObjectDisAppear(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectFreeState:
                OnNetMsg_NotifyGameObjectFreeState(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectRunState:
                OnNetMsg_NotifyGameObjectRunState(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBattleLoadingState:
                OnNetMsg_NotifyBattleStart(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyHeroList:
                OnNetMsg_NotifyHeroList(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyCSHeroList:
                OnNetMsg_NotifyCSHeroList(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectPrepareSkillState:
                OnNetMsg_NotifyPrepareSkill(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectReleaseSkillState:
                OnNetMsg_NotifyReleaseSkill(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectUsingSkillState:
                OnNetMsg_NotifyLeadingSkill(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectLastingSkillState:
                OnNetMsg_NotifyLastingSkill(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectDeadState:
                OnNetMsg_NotifyGameObjectDeadState(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGoodsInf:
                OnNetMsg_NotifyGoodsInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_ChatInRoom:
                OnNetMsg_NotifyRoomChat(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyRoomBaseInfo:
                OnNetMsg_NotifyRoomBaseInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyTryToChooseHero:
                OnNetMsg_NotifyToChooseHero(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBattleHeroInfo:
                OnNetMsg_NotifyBattleHeroInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyHPChange:
                OnNetMsg_NotifyHPChange(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyMPChange:
                OnNetMsg_NotifyMPChange(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillEntityInfo:
                OnNetMsg_NotifySkillUnitInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyAFPData:
                OnNetMsg_NotifyFightPropertyInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyHPInfo:
                OnNetMsg_NotifyHpInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyMPInfo:
                OnNetMsg_NotifyMpInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyHeroInfo:
                OnNetMsg_NotifyHeroInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillInfo:
                OnNetMsg_NotifySkillInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBattleFinish:
                OnNetMsg_NotifyBattleFinish(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyExpInfo:
                OnNetMsg_NotifyExpInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyHeroLevelInfo:
                OnNetMsg_NotifyLvInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyUserGameInfo:
                OnNetMst_NotifyUserGameInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyAbsorbBegin:
                OnNetMsg_NotifyAbsorbBegin(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyAbsorbMonsterResult:
                OnNetMsg_NotifyAbsorbMonsterResult(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyFuryValue:
                OnNetMsg_NotifyFuryValue(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyFuryState:
                OnNetMsg_NotifyFuryState(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyHeroKills:
                OnNetMsg_NotifyHeroKills(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyCurDeadTimes:
                OnNetMsg_NotifyCurDeadTimes(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyCurCP:
                OnNetMsg_NotifyCurCP(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyHeroRebornTimes:
                OnNetMsg_NotifyHeroRebornTimes(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBuyRebornSuccess:
                OnNetMsg_NotifyBuyRebornSuccess(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_BroadcastBattleHeroInfo:
                OnNetMsg_BroadcastBattleHeroInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyPersonalCPChange:
                OnNetMsg_NotifyCPChange(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_BroadCurBattleResult:
                OnNetMsg_BroadCurBattleResult(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_BroadBattlePersonalResult:
                OnNetMsg_BroadCurBattlePersonalResult(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_BroadBuildingDestroyByWho:
                OnNetMst_BroadBuildingDestroyByWho(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBornSolder:
                OnNotifyBornSoldier(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillEnd:
                //OnNetMsg_NotifySkillEnd(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyAltarBSOk:
                OnNetMsg_NotifyAltarBSIco(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectReleaseSkill://新技能释放,吟唱
                //OnNetMsg_NotifyGameOjectReleaseSkill(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectSkillCD://新的技能冷却控制
                OnNetMsg_NotifyGameObjectSkillCD(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelEmit://新飞行物体
                OnNetMsg_NotifySkillModelEmit(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelEmitDestroy://新飞行物体销毁
                OnNetMsg_NotifySkillModelEmitDestroy(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelHitTarget://新技能受击
                OnNetMsg_NotifySkillModelHitTarget(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelRange://范围技能
                OnNetMsg_NotifySkillModelRange(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelRangeEnd://范围技能结束
                OnNetMsg_NotifySkillModelRangeEnd(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelLeading://技能引导
                OnNetMsg_NotifySkillModelLeading(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelSummonEffect://召唤效果
                OnNetMsg_NotifySkillModelSummonEffect(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelBufEffect://buff效果
                OnNetMsg_NotifySkillModelBuf(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySummonLifeTime://召唤物时间协议
                OnNetMsg_NotifySummonLifeTime(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelStartForceMove:
                OnNetMsg_NotifySkillModelStartForceMove(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelStartForceMoveStop:
                OnNetMsg_NotifySkillModelStartForceMoveStop(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyLeaveBattleSuccess:
                OnNetMsg_NotifyLeaveBattleSuccess();
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBattleManagerChange:
                OnNetMsg__NotifyBattleManagerChange();
                break;
            case (Int32)GSToGC.MsgID.eMsgToSSFromCS_NotifyBornObj:
                OnNetMsg_NotifyBornObj(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelStartForceMoveTeleport:
                OnNetMsg_NotifySkillModelStartForceMoveTeleport(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_WarningToSelectHero:
                OnNetMsg_WarningToSelectHero();
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyPassitiveSkillRelease:
                OnNetMsg_NotifySkillPassitive(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyOBAppear:
                OnNetMsg_NotifyOBAppear(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyPassitiveSkillLoad:
                OnNetMsg_NotifySkillPassitiveLoad(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyPassitiveSkillUnload:
                OnNetMsg_NotifySkillPassitiveUnLoad(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillModelEmitTurn:
                OnNetMsg_NotifySkillModelEmitTurn(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGameObjectReliveState:
                OnNetMsg_NotifyGameObjectReliveState(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyKillNPC:
                OnNetMsg__NotifyKillNPC(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBlastHurt:
                OnNetMsg_NotifyBlastHurt(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGetNewCommodity:
                OnNetMsg_NotifyGetNewCommodity(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGoodsCfgInfo:
                OnNetMsg_NotifyGoodsCfgInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifySkillUpLv:
                OnNetMsg_NotifySkillUpLv(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromCS_NotifyGuideLastComStep:
                OnNetMsg_NotifyNewsGuideReConnect(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromCS_NotifyReconnectInfo:
                OnNetMsg_NotifyReconnectInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyOBReturnBattleRoom:
                OnNetMsg_NotifyOBReturnBattleRoom();
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyUserSNSList:
                OnNetMsg_NotifyUserFriendsList(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyUserSNSListChange:
                OnNetMsg_NotifyUserRemoveList(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyMsgFromUser:
                OnNetMsg_NotifyMsgFromUser(stream);
                break;
            //以后做
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_UserBeInvitedToBattle:
                OnNetMsg_UserBeInvitedToBattle(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBeAddFriendMsg:
                OnNetMsg_NotifyBeAddFriendMsg(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyQueryNickNameRet:
                OnNetMsg_NotifyQueryNickNameRet(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyUserGuideSetups:
                OnNetMsg_NotifyUserGuideSetUps(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyHeroReborn:
                OnNetMsg_NotifyHeroReborn(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGuideTips:
                OnNetMsg_NotifySendSoldierTip(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGs_NotifySecondaryGuideTask:
                OnNetMsg_NotifySecondaryGuide(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyNotice:
                OnNetMsg_NotifyNotice(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyUserCLDays:
                OnNetMsg_NotifyCLdays(stream);
                break;
            case (Int32)LSToGC.MsgID.eMsgToGCFromLS_NotifyLoginResult:
                OnNetMsg_NotifySdkLoginResult(stream);
                break;
            case (Int32)LSToGC.MsgID.eMsgToGCFromLS_NotifyServerBSAddr:
                OnNetMsg_NotifyServerAddr(stream);
                break;
            case (Int32)BSToGC.MsgID.eMsgToGCFromBS_OneClinetLoginCheckRet:
                OnNet_OneClinetLoginCheckRet(stream);
                break;
            case (Int32)BSToGC.MsgID.eMsgToGCFromBS_AskGateAddressRet:
                OnNet_NotifyGateServerInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGs_NotifyCurGold:
                OnNet_NotifyCurGold(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGs_NotifyCurDiamond:
                OnNet_NotifyCurDiamond(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGetloginReward_Hero:
                OnNetMsg_NotifyGetRewardHero(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGetloginReward_Skin:
                OnNetMsg_NotifyGetRewardSkin(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyGetloginReward_Rune:
                OnNetMsg_NotifyGetRewardRune(stream);
                break;
            //账号升级
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyUserBaseUpLv:
                OnNetMsg_NotifyUserBaseUpLv(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBattleSpanTime:
                OnNetMsg_NotifyBattleSpanTime(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyBattleMatherCount:
                OnNetMsg_NotifyMatchNumber(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyHeroAssist:
                OnNetMsg_NotifyHeroAssist(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyHeroAttributes:
                OnNetMsg_NotifyHeroAttributes(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromBS_NotifyCanInviteFriends:
                OnNetMsg_NotifyCanInviteFriends(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_AskRoomListRet:
                OnNetMsg_NotifyRoomList(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromBS_NotifyCurLastHitNum:
                OnNetMsg_NotifyCurLastHitNum(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyUserMail:
                OnNetMsg_NotifyUserMail(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyMailInfo:
                Debug.LogWarning("eMsgToGCFromGS_NotifyMailInfo:" + GSToGC.MsgID.eMsgToGCFromGS_NotifyMailInfo);
                OnNetMsg_NotifyMailInfo(stream);
                break;
            case (Int32)GSToGC.MsgID.eMsgToGCFromGS_NotifyIfMailDelAndSort:
                OnNetMsg_NotifyDelAndSortMail(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromBS_NotifyHerosInfo:
                OnNetMsg_NotifyHerosInfo(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyMatchTeamBaseInfo:
                OnNetMsg_NotifyMatchTeamBaseInfo(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyMatchTeamPlayerInfo:
                OnNetMsg_NotifyMatchTeamPlayerInfo(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyMatchTeamSwitch:
                OnNetMsg_NotifyMatchTeamSwitch(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyMatchInviteJoin:
                OnNetMsg_NotifyMatchInviteJoin(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyNewNickname:
                OnNetMsg_NotifyNewNickname(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyOneMatchNeedOne:
                OnNotifyOneMatchNeedOne(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyNewHeaderid:
                OnNetMsg_NotifyNewHeadID(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyRunesList:
                OnNetMsg_NotifyRunesList(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_UnloadRune:
                OnNetMsg_UnloadRune(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyBattleDelayTime:
                OnNetMsg_NotifyBattleDelayTime(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyRemoveCommodity:
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_GuideResp:
                OnNetMsg_NotifyGuideResp(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyOtherItemInfo:
                OnNetMsg_NotifyOtherItemInfo(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyUserLvInfo:
                NotifyUserLvInfo(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromCS_GuideKillsInfo:
                OnNetMsg_NotifyGuideKillsInfo(stream);
                break;
                //mail
            case (int)GSToGC.MsgID.eMsgToGCFromGS_NotifyMailRet:
                OnNetMsg_NotifyMailRet(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_UpdateAllTask:
                OnNetMsg_UpdateAllTask(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_UpdateAllDailyTask:
                OnNetMsg_UpdateAllDailyTask(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_AddOneTask:
                OnNetMsg_AddOneTask(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_UpdateOneTask:
                OnNetMsg_UpdateOneTask(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_DelOneTask:
                OnNetMsg_DelOneTask(stream);
                break;
            case (int)GSToGC.MsgID.eMsgToGCFromGS_RewardsOneTask:
                OnNetMsg_RewardsOneTask(stream);
                break;
        }
    }

    Int32 OnNetMsg_UpdateAllTask(Stream stream)
    {
        GSToGC.NotifyUpdateAllTask pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        DailyBonusCtrl.Instance.mDailyTaskDic.Clear();
        DailyBonusCtrl.Instance.mInfiniteTaskDic.Clear();
        foreach (GSToGC.TaskData one in pMsg.taskList)
        {
            STaskConfig taskConfig = null;
            taskConfig = ConfigReader.GetDailyTaskInfo(one.task_id);
            if (taskConfig != null)
            {
                CTask oneTask = new CTask();
                oneTask.mGuid = one.task_guid;
                oneTask.mCurCount = one.task_curCount;
                oneTask.mConfig = taskConfig;
                if (oneTask.mCurCount == taskConfig.taskMaxCount) DailyBonusCtrl.Instance.mIsHadTaskFinished = true;
                DailyBonusCtrl.Instance.mDailyTaskDic.Add(oneTask.mGuid,oneTask);
                continue;
            }
            taskConfig = ConfigReader.GetInfiniteTaskInfo(one.task_id);
            if (taskConfig != null)
            {
                CTask oneTask = new CTask();
                oneTask.mGuid = one.task_guid;
                oneTask.mCurCount = one.task_curCount;
                oneTask.mConfig = taskConfig;
                DailyBonusCtrl.Instance.mInfiniteTaskDic.Add(oneTask.mGuid, oneTask);
                continue;
            }
        }
        DailyBonusCtrl.Instance.mIsHadNewDailyTask = pMsg.IsHadNewDailyTask;
        EventCenter.Broadcast(EGameEvent.eGameEvent_NotifyAllTaskUpdate);
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_UpdateAllDailyTask(Stream stream)
    {
        GSToGC.NotifyUpdateAllDailyTask pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        DailyBonusCtrl.Instance.mDailyTaskDic.Clear();
        foreach (GSToGC.TaskData one in pMsg.taskList)
        {
            STaskConfig taskConfig = null;
            taskConfig = ConfigReader.GetDailyTaskInfo(one.task_id);
            if (taskConfig != null)
            {
                CTask oneTask = new CTask();
                oneTask.mGuid = one.task_guid;
                oneTask.mCurCount = one.task_curCount;
                oneTask.mConfig = taskConfig;
                if (oneTask.mCurCount == taskConfig.taskMaxCount) DailyBonusCtrl.Instance.mIsHadTaskFinished = true;
                DailyBonusCtrl.Instance.mDailyTaskDic.Add(oneTask.mGuid, oneTask);
                continue;
            }
        }
        DailyBonusCtrl.Instance.mIsHadNewDailyTask = pMsg.IsHadNewDailyTask;
        EventCenter.Broadcast(EGameEvent.eGameEvent_NotifyDailyTaskUpdate);
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_AddOneTask(Stream stream)
    {
        GSToGC.NotifyAddOneTask pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        STaskConfig taskConfig = ConfigReader.GetDailyTaskInfo(pMsg.oneTask.task_id);
        if (taskConfig != null)
        {
            CTask oneTask = new CTask();
            oneTask.mGuid = pMsg.oneTask.task_guid;
            oneTask.mCurCount = pMsg.oneTask.task_curCount;
            oneTask.mConfig = taskConfig;
            DailyBonusCtrl.Instance.mDailyTaskDic.Add(oneTask.mGuid, oneTask);
            EventCenter.Broadcast<CTask>(EGameEvent.eGameEvent_NotifyOneTaskAdd, oneTask);
        }
        taskConfig = ConfigReader.GetInfiniteTaskInfo(pMsg.oneTask.task_id);
        if (taskConfig != null)
        {
            CTask oneTask = new CTask();
            oneTask.mGuid = pMsg.oneTask.task_guid;
            oneTask.mCurCount = pMsg.oneTask.task_curCount;
            oneTask.mConfig = taskConfig;
            DailyBonusCtrl.Instance.mInfiniteTaskDic.Add(oneTask.mGuid, oneTask);
            EventCenter.Broadcast<CTask>(EGameEvent.eGameEvent_NotifyOneTaskAdd, oneTask);
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_UpdateOneTask(Stream stream)
    {
        GSToGC.NotifyUpdateOneTask pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        STaskConfig taskConfig = ConfigReader.GetDailyTaskInfo(pMsg.oneTask.task_id);
        if (taskConfig != null)
        {
            foreach (KeyValuePair<uint,CTask> oneTask in DailyBonusCtrl.Instance.mDailyTaskDic)
            {
                if (oneTask.Value.mGuid == pMsg.oneTask.task_guid)
                {
                    oneTask.Value.mCurCount = pMsg.oneTask.task_curCount;
                    if (oneTask.Value.mCurCount == oneTask.Value.mConfig.taskMaxCount) DailyBonusCtrl.Instance.mIsHadTaskFinished = true;
                    EventCenter.Broadcast<CTask>(EGameEvent.eGameEvent_NotifyOneTaskUpdate, oneTask.Value);
                    break;
                }
            }
        }
        taskConfig = ConfigReader.GetInfiniteTaskInfo(pMsg.oneTask.task_id);
        if (taskConfig != null)
        {
            foreach (KeyValuePair<uint, CTask> oneTask in DailyBonusCtrl.Instance.mInfiniteTaskDic)
            {
                if (oneTask.Value.mGuid == pMsg.oneTask.task_guid)
                {
                    oneTask.Value.mCurCount = pMsg.oneTask.task_curCount;
                    EventCenter.Broadcast<CTask>(EGameEvent.eGameEvent_NotifyOneTaskUpdate, oneTask.Value);
                    break;
                }
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_DelOneTask(Stream stream)
    {
        GSToGC.NotifyDelOneTask pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (KeyValuePair<uint, CTask> oneTask in DailyBonusCtrl.Instance.mDailyTaskDic)
        {
            if (oneTask.Value.mGuid == pMsg.task_guid)
            {
                EventCenter.Broadcast<CTask>(EGameEvent.eGameEvent_NotifyOneTaskDel, oneTask.Value);
                DailyBonusCtrl.Instance.mDailyTaskDic.Remove(pMsg.task_guid);
                return (Int32)EErrorCode.eNormal;
            }
        }
        foreach (KeyValuePair<uint, CTask> oneTask in DailyBonusCtrl.Instance.mInfiniteTaskDic)
        {
            if (oneTask.Value.mGuid == pMsg.task_guid)
            {
                EventCenter.Broadcast<CTask>(EGameEvent.eGameEvent_NotifyOneTaskDel, oneTask.Value);
                DailyBonusCtrl.Instance.mInfiniteTaskDic.Remove(pMsg.task_guid);
                return (Int32)EErrorCode.eNormal;
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_RewardsOneTask(Stream stream)
    {
        GSToGC.NotifyRewardsOneTask pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (KeyValuePair<uint, CTask> oneTask in DailyBonusCtrl.Instance.mDailyTaskDic)
        {
            if (oneTask.Value.mGuid == pMsg.task_guid)
            {
                EventCenter.Broadcast<CTask>(EGameEvent.eGameEvent_NotifyOneTaskRewards, oneTask.Value);
                return (Int32)EErrorCode.eNormal;
            }
        }
        foreach (KeyValuePair<uint, CTask> oneTask in DailyBonusCtrl.Instance.mInfiniteTaskDic)
        {
            if (oneTask.Value.mGuid == pMsg.task_guid)
            {
                EventCenter.Broadcast<CTask>(EGameEvent.eGameEvent_NotifyOneTaskRewards, oneTask.Value);
                return (Int32)EErrorCode.eNormal;
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //
    //邮件操作返回值
    ///
    Int32  OnNetMsg_NotifyMailRet(Stream stream)
    {
        GSToGC.NotifyMailRet pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        int mailId = pMsg.mailid;
        Int32 errocode = pMsg.errcode;
        //给出提示
        MsgInfoManager.Instance.ShowMsg(errocode);
        //删除该id邮件
        Debug.Log("-----error mailid--------" + mailId); 
        MailCtrl.Instance.DelOrSortMailList(mailId, true, true);
        return (Int32)EErrorCode.eNormal;
    }
    /// <summary>
    /// 引导击杀信息
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    Int32 OnNetMsg_NotifyGuideKillsInfo(Stream stream)
    {
        GSToGC.GuideKillsInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sGUID = (ulong)pMsg.guid;// (ulong)pMsg.msgid;
        Int32 npcType = pMsg.npctype;
        EventCenter.Broadcast<int>(EGameEvent.eGameEvent_GuideKillTask, npcType);
        Debug.Log("-----sGUID:" + sGUID + ";npcType:" + npcType); 
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyOtherItemInfo(Stream stream)
    {
        GSToGC.NotifyOtherItemInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        GoodsModel.Instance.AddOrChangeRuneBaptze(pMsg.item);
        return (Int32)EErrorCode.eNormal;
    }

    Int32 NotifyUserLvInfo(Stream stream)
    {
        GSToGC.NotifyUserLvInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        short level = (short)pMsg.curlv;
        GameUserModel.Instance.SetGameUserLv(level);
        GameUserModel.Instance.SetGameUserExp((int)pMsg.curexp);
        return 0;
    }

    /// <summary>
    /// 新手引导完成信息
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    Int32 OnNetMsg_NotifyGuideResp(Stream stream)
    {
        GSToGC.GuideCSStepInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        bool comp = pMsg.allcomp;
        UIGuideModel.Instance.mIsGuideComp = comp;
        if (!comp)
        {
            UIGuideCtrl.Instance.GuideRespStep(pMsg);
        }

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyRemoveCommodity(Stream stream)
    {
        GSToGC.NotifyRemoveCommodity pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        Debug.Log("收到删除商品事件！");

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyBattleDelayTime(Stream stream)
    {
        GSToGC.BattleDelayTime pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        GamePlayCtrl.Instance.BattleDelayTimeBegin(pMsg.delayTime / 1000);

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_UnloadRune(Stream stream)
    {
        GSToGC.UnloadRune pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        RuneEquipCtrl.Instance.UnloadRune(pMsg.page, pMsg.pos);

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyRunesList(Stream stream)
    {
        GSToGC.NotifyRunesList pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        for (int i = 0; i < pMsg.runes_slot_info.Count; ++i)
        {
            GSToGC.RunesSlot sSlotInfo = pMsg.runes_slot_info[i];
            RuneEquipModel.Instance.UpdateRuneSlotInfo(sSlotInfo.runeid, sSlotInfo.page, sSlotInfo.slotpos);
        }

        for (int i = 0; i < pMsg.runesbaginfo.Count; ++i)
        {
            GSToGC.RunesBagInfo sBagInfo = pMsg.runesbaginfo[i];
            MarketRuneListCtrl.Instance.UpdateRuneBagInfo(sBagInfo.runeid, sBagInfo.num, sBagInfo.gottime);
        }

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyNewHeadID(Stream stream)
    {
        GSToGC.NotifyNewHeaderid pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        PresonInfoCtrl.Instance.SetHeadID(pMsg.guid, pMsg.newheaderid);
        PresonInfoCtrl.Instance.ChangeHeadID();
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyNewNickname(Stream stream)
    {
        GSToGC.NotifyNewNickname pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        PresonInfoCtrl.Instance.SetNickName(pMsg.guid, pMsg.newnickname);
        PresonInfoCtrl.Instance.ChangeNickName();
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNotifyOneMatchNeedOne(Stream stream)
    {
        GSToGC.NotifyOneMatchNeedOne pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        TeamMatchCtrl.Instance.ShowServerInvitation(pMsg.mapid, pMsg.fightid);

        return (Int32)EErrorCode.eNormal;
    }



    /// <summary>
    /// 邮件
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    Int32 OnNetMsg_NotifyDelAndSortMail(Stream stream)
    {
        GSToGC.DelAndSortMail pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        //todo:
        Debug.Log("-----GSToGC.DelAndSortMail pMsg--------");
        MailCtrl.Instance.DelOrSortMailList(pMsg);
        return (Int32)EErrorCode.eNormal;
    }
    /// 单封邮件内容：标题,内容，赠送
    Int32 OnNetMsg_NotifyMailInfo(Stream stream)
    {
        GSToGC.MailInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        //todo:
        MailCtrl.Instance.UpdateMailInfo(pMsg);
        return (Int32)EErrorCode.eNormal;
    }
    //邮件列表//
    Int32 OnNetMsg_NotifyUserMail(Stream stream)
    {
        GSToGC.NotifyMailList pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        //todo:
        MailCtrl.Instance.AddMail(pMsg);
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyHerosInfo(Stream stream)
    {
        GSToGC.NotifyHerosInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return (Int32)EErrorCode.eNormal;
        }
        foreach (var item in pMsg.info)
        {
            UInt64 sGUID;
            sGUID = (ulong)item.guid;
            BattleingData.Instance.AddInitPlayer(sGUID, item.nickname, item.killnum, item.deadtimes, item.asstimes, item.herolv, item.lasthit, (EntityCampType)item.camgpid, (int)item.heroid);
            foreach (var goods in item.goods)
            {
                BattleingData.Instance.AddPlayer(sGUID, 0, BattleDataType.Goods, goods.grid, goods.goodid);
            }
        }
        BattleInfoCtrl.Instance.Enter();
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyRoomList(Stream stream)
    {
        GSToGC.AskRoomListRet pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return (Int32)EErrorCode.eNormal;
        }

        BattleCtrl.Instance.UpdateRoomList(pMsg.roomlist);

        return (Int32)EErrorCode.eNormal;
    }


    Int32 OnNetMsg_NotifyCurLastHitNum(Stream stream)
    {
        GSToGC.LastHitNum pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return (Int32)EErrorCode.eNormal;
        }
        UInt64 sguid;
        sguid = pMsg.guid;
        BattleingData.Instance.AddPlayer(sguid, pMsg.lhnum, BattleDataType.LastHit);
        //EventCenter.Broadcast(EGameEvent.eGameEvent_AllPlayerLastHit);
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyCanInviteFriends(Stream stream)
    {
        GSToGC.CanInviteFriends pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return (Int32)EErrorCode.eNormal;
        }
        if (pMsg.friends.Count == 0)
        {
            MsgInfoManager.Instance.ShowMsg(10033);
            return (Int32)EErrorCode.eNormal;
        }
        foreach (var item in pMsg.friends)
        {
            FriendManager.Instance.AddInvite(item.guididx, item.HeaderId, item.nickname);
        }
        RoomCtrl.Instance.OpenInviteList();
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyHeroAttributes(Stream stream)
    {
        GSToGC.HeroAttributes pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sguid;
        sguid = pMsg.guid;
        BattleingData.Instance.SetAttributes(pMsg.PlayerSpeed, pMsg.AttackInterval, pMsg.AttackRange, pMsg.ResurgenceTime, pMsg.PhysicAttack, pMsg.SpellsAttack, pMsg.PhysicDef, pMsg.SpellsDef);
        EventCenter.Broadcast(EGameEvent.eGameEvent_HeroAttributesInfo);
        return (Int32)EErrorCode.eNormal;
    }
    Int32 OnNetMsg_NotifyHeroAssist(Stream stream)
    {
        GSToGC.HeroAssist pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sguid;
        sguid = pMsg.guid;
        BattleingData.Instance.AddPlayer(sguid, pMsg.assist, BattleDataType.Assist);
        EventCenter.Broadcast(EGameEvent.eGameEvent_LocalPlayerAssist);
        //EventCenter.Broadcast(EGameEvent.eGameEvent_AllPlayerAssist);
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyMatchNumber(Stream stream)
    {
        GSToGC.BattleMatcherCount pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        EventCenter.Broadcast<int, int>(EGameEvent.eGameEvent_MatchNumber, pMsg.count, pMsg.maxcount);
        return (Int32)EErrorCode.eNormal;
    }
    Int32 OnNetMsg_NotifyBattleSpanTime(Stream stream)
    {
        GSToGC.BattleSpanTime pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        GameTimeData.Instance.UpdatePlayTime(pMsg.spanTime);
        EventCenter.Broadcast<long>(EGameEvent.eGameEvent_GameStartTime, pMsg.spanTime);
        return (Int32)EErrorCode.eNormal;
    }
    Int32 OnNetMsg_NotifyUserBaseUpLv(Stream stream)
    {
        GSToGC.UserBaseUpLv pMsg = ProtoBuf.Serializer.Deserialize<GSToGC.UserBaseUpLv>(stream);

        CEvent eve = new CEvent(EGameEvent.eGameEent_UpLv);
        eve.AddParam("lv", pMsg.lv);
        EventCenter.SendEvent(eve);

        return (Int32)EErrorCode.eNormal;
    }
    Int32 OnNetMsg_NotifyGetRewardRune(Stream stream)
    {
        GSToGC.GetloginReward_Rune pMsg = ProtoBuf.Serializer.Deserialize<GSToGC.GetloginReward_Rune>(stream);
        EventCenter.Broadcast(EGameEvent.eGameEent_RemoveDailyBonus);
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyGetRewardSkin(Stream stream)
    {
        GSToGC.GetloginReward_Skin pMsg = ProtoBuf.Serializer.Deserialize<GSToGC.GetloginReward_Skin>(stream);
        EventCenter.Broadcast(EGameEvent.eGameEent_RemoveDailyBonus);
        return (Int32)EErrorCode.eNormal;
    }
    Int32 OnNetMsg_NotifyGetRewardHero(Stream stream)
    {
        GSToGC.GetloginReward_Hero pMsg = ProtoBuf.Serializer.Deserialize<GSToGC.GetloginReward_Hero>(stream);
        EventCenter.Broadcast(EGameEvent.eGameEent_RemoveDailyBonus);
        return (Int32)EErrorCode.eNormal;
    }

    /// <summary>
    /// 玩家当前钻石
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    Int32 OnNet_NotifyCurDiamond(Stream stream)
    {
        GSToGC.NotifyCurDiamond pMsg = ProtoBuf.Serializer.Deserialize<GSToGC.NotifyCurDiamond>(stream);
        //GameUserDataCtrl.Instance.
        GameUserCtrl.Instance.GameUserCurDiamond(pMsg.Diamond);
        Debug.Log("OnNet_NotifyCurDiamond  " + pMsg.Diamond);
        return (Int32)EErrorCode.eNormal;
    }

    /// <summary>
    /// 玩家当前金币
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    Int32 OnNet_NotifyCurGold(Stream stream)
    {
        GSToGC.NotifyCurGold pMsg = ProtoBuf.Serializer.Deserialize<GSToGC.NotifyCurGold>(stream);
        GameUserCtrl.Instance.GameUserCurGold(pMsg.gold);
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNet_NotifyGateServerInfo(Stream stream)
    {
        LoginCtrl.Instance.RecvGateServerInfo(stream);

        return (Int32)EErrorCode.eNormal;
    }


    Int32 OnNet_OneClinetLoginCheckRet(Stream stream)
    {
        BSToGC.ClinetLoginCheckRet pMsg = ProtoBuf.Serializer.Deserialize<BSToGC.ClinetLoginCheckRet>(stream);
        UInt32 loginSuccess = pMsg.login_success;
        if (loginSuccess != 1)//fail
        {
            LoginCtrl.Instance.LoginFail();
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyServerAddr(Stream stream)
    {
        LoginCtrl.Instance.UpdateServerAddr(stream);

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifySdkLoginResult(Stream stream)
    {
        LSToGC.LoginResult pcMsg = ProtoBuf.Serializer.Deserialize<LSToGC.LoginResult>(stream);
        LoginCtrl.Instance.LoginFail();
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyCLdays(Stream stream)
    {
        GSToGC.NotifyUserCLDays pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        DailyBonusCtrl.Instance.SetDayAwards(pMsg.month, pMsg.today, pMsg.totalCldays, pMsg.cldays, pMsg.isTodayCan);
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifyNotice(Stream stream)
    {
        GSToGC.GameNotice pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        if (pMsg.notice.Count == 0)
            return (Int32)EErrorCode.eNormal;
        SystemNoticeData.Instance.Clear();
        foreach (var item in pMsg.notice)
        {
            if (string.IsNullOrEmpty(item.notice))
            return (Int32)EErrorCode.eNormal;
            SystemNoticeData.Instance.SetSystemNotList(item.title, (NoticeIdentify)item.flag, (NoticeState)item.status, (int)item.priority, item.notice);
        }
        if (UIGuideModel.Instance.mIsGuideComp)
        {
            SystemNoticeCtrl.Instance.Enter();
        }
        return (Int32)EErrorCode.eNormal;
    }
    Int32 OnNetMsg_NotifyHeroReborn(Stream stream)
    {
        EventCenter.Broadcast(EGameEvent.eGameEvent_HeroReborn);
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifyQueryNickNameRet(Stream stream)
    {
        GSToGC.NotifyQueryNickNameRet pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        EventCenter.Broadcast(EGameEvent.eGameEvent_FindClear);
        if (pMsg.info.Count == 0)
        {
            MsgInfoManager.Instance.ShowMsg(40029);
            return (Int32)EErrorCode.eNormal;
        }
        foreach (GSToGC.NotifyQueryNickNameRet.QueryInfo info in pMsg.info)
        {
            string headID = info.headid.ToString();
            string head = null;
            if (!FriendManager.Instance.AllSearch.TryGetValue(info.nickname, out head))
            {
                FriendManager.Instance.AllSearch.Add(info.nickname, headID);
            }
            EventCenter.Broadcast<string, string>(EGameEvent.eGameEvent_FindFriendInfo, info.nickname, headID);
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifyBeAddFriendMsg(Stream stream)
    {
        GSToGC.NotifyBeAddFriendMs pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        //if (!IGuideTaskManager.Instance().IsLineTaskFinish())
        //{
        //    return (Int32)EErrorCode.eNormal;
        //}
        //ToReview sendnickname没用上，send和rec顺序不一定正确

        LobbyCtrl.Instance.InviteInfo(pMsg.sdnder_guididx, pMsg.sendnickname);

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_UserBeInvitedToBattle(Stream stream)
    {
        GSToGC.UserBeInvitedToBattle pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        InviteOtherPlayer.Instance.SetInviteion((ulong)pMsg.battleid, pMsg.pwd.ToString(), pMsg.pwd.Length, pMsg.Invitor);
        EventCenter.Broadcast(EGameEvent.eGameEvent_NewInviteRoom);
        EventCenter.Broadcast<string>(EGameEvent.eGameEvent_InviteAddRoom, pMsg.Invitor);
        return (Int32)EErrorCode.eNormal;
    }
    /// <summary>
    /// 聊天消息
    /// </summary>
    /// <param name="pcMsg"></param>
    /// <returns></returns>
    //已换
    Int32 OnNetMsg_NotifyMsgFromUser(Stream stream)
    {
        GSToGC.NotifyMsgFromUser pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        //if (!IGuideTaskManager.Instance().IsLineTaskFinish())
        //{
        //    return (Int32)EErrorCode.eNormal;
        //}
        IChat chat = null;
        if (!FriendManager.Instance.AllTalkDic.ContainsKey(pMsg.guididx))
        {
            chat = new IChat();
            chat.SetMsgInfo(pMsg.guididx, pMsg.nickname, pMsg.chatstr, MsgTalk.UnReadMsg, pMsg.headid, false);
            FriendManager.Instance.AllTalkDic.Add(pMsg.guididx, chat);
        }
        else
        {
            chat = FriendManager.Instance.AllTalkDic[pMsg.guididx];
            chat.SetMsgInfo(pMsg.guididx, pMsg.nickname.Trim(), pMsg.chatstr, MsgTalk.UnReadMsg, pMsg.headid, false);
        }
        EventCenter.Broadcast<bool>(EGameEvent.eGameEvent_ReceiveLobbyMsg, true);
        EventCenter.Broadcast<UInt64>(EGameEvent.eGameEvent_ReceiveNewMsg, pMsg.guididx);
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    //消息名不一致eMsgToGCFromGS_NotifyUserRemovesList->eMsgToGCFromGS_NotifyUserSNSListChange
    Int32 OnNetMsg_NotifyUserRemoveList(Stream stream)
    {
        //ToReview 比原来多了循环
        GSToGC.NotifyUserSNSListChange pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        FRIENDTYPE type = (FRIENDTYPE)pMsg.type;
        EventCenter.Broadcast<UInt64>(EGameEvent.eGameEvent_RemoveFriend, pMsg.guididx);

        if (type == FRIENDTYPE.FRIENDLIST)
        {
            FriendManager.Instance.DelFriend(pMsg.guididx);
        }
        else if (type == FRIENDTYPE.BLACKLIST)
        {
            FriendManager.Instance.DelBlackList(pMsg.guididx);
        }
        EventCenter.Broadcast<UInt64>(EGameEvent.eGameEvent_RemoveFriendEnd, pMsg.guididx);
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    //协议名不一致eMsgToGCFromGS_NotifyUserFriendsList->eMsgToGCFromGS_NotifyUserSNSList
    Int32 OnNetMsg_NotifyUserFriendsList(Stream stream)
    {
        GSToGC.NotifyUserSNSList pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (GSToGC.SNSInfo info in pMsg.info)
        {
            int headID = (int)info.headid;
            byte isOnLine = (byte)info.status;
            Friend friend = null;
            FRIENDTYPE type = (FRIENDTYPE)info.type;
            bool isFriend = true;
            if (type == FRIENDTYPE.FRIENDLIST)
            {
                isFriend = true;
                if (!FriendManager.Instance.AllFriends.TryGetValue(info.guididx, out friend))
                {
                    friend = new Friend();
                    friend.SetFriendInfo(info.guididx, info.nickname, headID, isOnLine == 1, info.viplv);
                    FriendManager.Instance.AddFriend(info.guididx, friend);
                    EventCenter.Broadcast<Friend>(EGameEvent.eGameEvent_CreateFriendPrefab, friend);
                }
                else
                {
                    friend.SetFriendInfo(info.guididx, info.nickname, headID, isOnLine == 1, info.viplv);
                    EventCenter.Broadcast(EGameEvent.eGameEvent_FriendChangeInfo, info.guididx);
                }
            }
            else if (type == FRIENDTYPE.BLACKLIST)
            {
                isFriend = false;
                if (!FriendManager.Instance.AllBlackDic.TryGetValue(info.guididx, out friend))
                {
                    friend = new Friend();
                    friend.SetFriendInfo(info.guididx, info.nickname, headID, false);
                    FriendManager.Instance.AddOnLineBlackList(info.guididx, friend);
                    EventCenter.Broadcast(EGameEvent.eGameEvent_CreateBlackPrefab);
                }//暂时先注掉。黑名单暂时无在线状态
                //else
                //{
                //    friend.SetFriendInfo(info.guididx, info.nickname, headID, false);
                //    EventCenter.Broadcast(EGameEvent.eGameEvent_FriendChangeInfo, info.guididx);
                //}
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换（不需要参数）
    Int32 OnNetMsg_NotifyOBReturnBattleRoom()
    {
        CEvent evt = new CEvent(EGameEvent.eGameEvent_Loading);
        evt.AddParam("NextState", BlGame.GameState.GameStateType.GS_Room);
        EventCenter.SendEvent(evt);

        return (Int32)EErrorCode.eNormal;
    }

    //祭坛头像是否成功
    Int32 OnNetMsg_NotifyAltarHeadIcon(CMsg pcMsg)
    {
        UInt32 m_AltarId = pcMsg.GetUInt32();
        Int32 altrId = pcMsg.GetInt32();
        Int32 soderType = pcMsg.GetInt32();
        Debug.LogError("ji tan");
        //AltarManager.Instance.ShowAltarHead(altrId, soderType);
        //if (UIAltarSelect.Instance != null)
        //    UIAltarSelect.Instance.Destoys();

        return (Int32)EErrorCode.eNormal;
    }

    //GS通知OB 对象技能升级
    //已换
    Int32 OnNetMsg_NotifySkillUpLv(Stream stream)
    {
        GSToGC.NotifySkillUpLv pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sGuid;
        sGuid = pMsg.guid;
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sGuid, out entity))
            if (entity is Iplayer)
            {
                PlayerSkillData.Instance.SetPlayerSkillInfo((Iplayer)entity, (int)pMsg.skillpos, (int)pMsg.skillid); //ToReview uint->int
                EventCenter.Broadcast<Iplayer>(EGameEvent.eGameEvent_SkillUpLvChange, (Iplayer)entity);
            }
        return (Int32)EErrorCode.eNormal;
    }

    //补兵
    //已换
    //协议名不一致eMsgToSSFromCS_NotifyKillNPC->eMsgToGCFromGS_NotifyKillNPC
    Int32 OnNetMsg__NotifyKillNPC(Stream stream)
    {
        //UInt64 sMasterGUID = pcMsg.GetGUID();
        //UInt32 DI = pcMsg.GetUInt32();
        //Iplayer player = PlayerManager.Instance.LocalPlayer;
        //if (UIViewerBattleInfo.Instance != null)
        //{
        //    UIViewerBattleInfo.Instance.SetFarmInfo(sMasterGUID, (int)DI);
        //}
        //return (Int32)EErrorCode.eNormal;

        GSToGC.NotifyKillNPC pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sMasterGUID;
        sMasterGUID = pMsg.guid;
        Iplayer player = PlayerManager.Instance.LocalPlayer;
        if (UIViewerBattleInfo.Instance != null)
        {
            UIViewerBattleInfo.Instance.SetFarmInfo(sMasterGUID, (int)pMsg.killnum);    //ToReview uint->int
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    //协议名不一致eMsgToSSFromCS_NotifyBlastHurt->eMsgToGCFromGS_NotifyBlastHurt
    Int32 OnNetMsg_NotifyBlastHurt(Stream stream)
    {
        GSToGC.NotifyBlastHurt pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 id;
        id = pMsg.guid;
        Ientity entity;
        Vector3 posInWorld = Vector3.zero;
        if (EntityManager.AllEntitys.TryGetValue(id, out entity))
            CrticalStrikeManager.Instance.CreateCrticalStrike(pMsg.blasthp, entity);
        return (Int32)EErrorCode.eNormal;
    }

    /// <summary>
    /// 帐号获得新的物品
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    Int32 OnNetMsg_NotifyGetNewCommodity(Stream stream)
    {
        GSToGC.NotifyGetNewCommodity pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        GameUserCtrl.Instance.GameUserGetNewCommodity((int)pMsg.Commodityid);
        return (Int32)EErrorCode.eNormal;
    }

    /// <summary>
    /// 获得商城所要出售的商品的信息
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    Int32 OnNetMsg_NotifyGoodsCfgInfo(Stream stream)
    {
        GSToGC.GoodsBuyCfgInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (GSToGC.GoodsCfgInfo cfgInfo in pMsg.info)
        {
            int gsId = cfgInfo.goodid;
            if (GameDefine.GameConstDefine.IfHeroBuyCfg(gsId))
            {
                if (ConfigReader.HeroBuyXmlInfoDict.ContainsKey(gsId))
                {
                    MarketHeroListModel.Instance.HeroListInfo(cfgInfo);
                }
            }
            else if (GameDefine.GameConstDefine.IfRuneBuyCfg(gsId))
            {
                MarketRuneListModel.Instance.AddRuneCfgListInfo(cfgInfo);
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifyGameObjectReliveState(Stream stream)
    {
        GSToGC.NotifyGameObjectReliveState pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sGUID;
        sGUID = pMsg.guid;
        Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 ford = this.ConvertDirToVector3(pMsg.dir);
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            pos.y = entity.RealEntity.transform.position.y;
            entity.GOSSI.sServerBeginPos = pos;
            entity.GOSSI.sServerSyncPos = pos;
            entity.GOSSI.sServerDir = ford;
            entity.GOSSI.fServerSpeed = entity.EntityFSMMoveSpeed;
            entity.GOSSI.fBeginTime = Time.realtimeSinceStartup;
            entity.GOSSI.fLastSyncSecond = Time.realtimeSinceStartup;
            entity.EntityFSMChangedata(pos, ford, entity.EntityFSMMoveSpeed);
            entity.OnFSMStateChange(EntityReliveFSM.Instance);
        }
        return (Int32)EErrorCode.eNormal;
    }

    /// <summary>
    /// Notify观察者英雄信息
    /// </summary>
    /// <param name="pcMsg"></param>
    /// <returns></returns>
    Int32 OnNetMsg_NotifyDefaultHeros(CMsg pcMsg)
    {
        Int32 m_total = pcMsg.GetInt32();
        for (int to = 0; to < m_total; to++)
        {
            UInt64 sGUID = pcMsg.GetGUID();
            int m_camp = pcMsg.GetInt32();
            UInt32 m_un32HeaderID = pcMsg.GetUInt32();
            UInt32 m_un32Lv = pcMsg.GetUInt32();
            UInt32 m_un32HP = pcMsg.GetUInt32();
            UInt32 m_un32MP = pcMsg.GetUInt32();
            Ientity entity;
            EntityManager.AllEntitys.TryGetValue(sGUID, out entity);
            EventCenter.Broadcast(EGameEvent.eGameEvent_HeroInfoChange, (Iplayer)entity);
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    //协议名不一致eMsgToSSFromCS_NotifyOBAppear->eMsgToGCFromGS_NotifyOBAppear
    Int32 OnNetMsg_NotifyOBAppear(Stream stream)
    {
        GSToGC.NotifyOBAppear pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        if (LoadScene.Instance != null)
        {
            LoadScene.Instance.CloseLoading();
        }
        //ToReview obid没用上
        if (PlayerManager.Instance.LocalAccount.ObType == ObPlayerOrPlayer.PlayerObType)
        {
            PlayerManager.Instance.LocalAccount.EntityCamp = GameMethod.GetEntityCamp(pMsg.camp);
        }
        if (UIViewer.Instance != null)
        {
            UIViewer.Instance.gameObject.AddComponent<UIDragObCamera>();
        }
        if (UIDragObCamera.Instance != null)
        {
            UIDragObCamera.Instance.SetUsable(true);
        }
        return (Int32)EErrorCode.eNormal;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pcMsg"></param>
    /// <returns></returns>
    //已换
    Int32 OnNetMsg_NotifySkillModelEmitTurn(Stream stream)
    {
        GSToGC.NotifySkillModelEmitTurn pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 master;
        master = pMsg.guid;
        //ToReview effectid,pos,dir,targuid,tarpos没用上
        IEffect effect = EffectManager.Instance.GetEffect(pMsg.progectid);
        if (effect != null)
        {
            FlyEffect fEffect = effect as FlyEffect;
            if (fEffect != null)
            {
                fEffect.isTurn = true;
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifySkillPassitiveLoad(Stream stream)
    {
        GSToGC.NotifyPassitiveSkillLoad pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 masterKey;
        masterKey = pMsg.guid;
        uint skillid = (uint)pMsg.skillid;  //ToReview int->uint
        SkillPassiveConfigInfo skillInfo = ConfigReader.GetSkillPassiveConfig(skillid);
        Ientity entity = null;
        EntityManager.AllEntitys.TryGetValue(masterKey, out entity);
        if (skillInfo != null && entity != null)
        {
            EffectManager.Instance.CreatePassitiveEffect(entity, skillid, (uint)pMsg.uniqueid); //ToReview int->uint
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifySkillPassitiveUnLoad(Stream stream)
    {
        GSToGC.NotifyPassitiveSkillUnLoad pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 masterKey;
        masterKey = pMsg.guid;
        //ToReview 原协议skillid没用上，原协议projectId在新协议中叫skillid?
        EffectManager.Instance.DestroyEffect(pMsg.uniqueid);
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    //协议名不一致eMsgToGCFromGS_NotifyPassitiveSkill->eMsgToGCFromGS_NotifyPassitiveSkillRelease
    Int32 OnNetMsg_NotifySkillPassitive(Stream stream)//触发，可以多次
    {
        GSToGC.NotifyPassitiveSkillRelease pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 masterKey;
        masterKey = pMsg.guid;
        SkillPassiveConfigInfo skillInfo = ConfigReader.GetSkillPassiveConfig((uint)pMsg.skillid);      //ToReview int->uint
        Ientity entity = null;
        EntityManager.AllEntitys.TryGetValue(masterKey, out entity);
        Iplayer player = PlayerManager.Instance.LocalPlayer;
        if (player != null && entity is Iplayer && player == entity)
        {
            EventCenter.Broadcast(EGameEvent.eGameEvent_LocalPlayerPassiveSkillsUpLv, pMsg.skillid, pMsg.timeLeft);
        }
        if (skillInfo != null && entity != null)
        {
            entity.RealEntity.PlayerAnimation(skillInfo.action);
            IEffect effect = EffectManager.Instance.CreateNormalEffect(GameConstDefine.LoadGameSkillEffectPath + "release/" + skillInfo.effect, entity.RealEntity.objPoint.gameObject);
            string soundPath = "";
            soundPath = GameConstDefine.LoadGameSoundPath + skillInfo.sound;

            ResourceUnit unit = ResourcesManager.Instance.loadImmediate(soundPath, ResourceType.ASSET);
            if (unit.Asset != null && effect != null)
            {
                AudioClip clip = unit.Asset as AudioClip;
                if (clip != null)
                {
                    AudioSource Audio = AudioManager.Instance.PlayEffectAudio(clip);
                    SceneSoundManager.Instance.addSound(Audio, effect.obj);
                }
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifySkillModelStartForceMoveTeleport(Stream stream)
    {
        GSToGC.NotifySkillModelStartForceMoveTeleport pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 masterKey;
        masterKey = pMsg.guid;
        Vector3 beginPos = this.ConvertPosToVector3(pMsg.beginpos);
        Vector3 beginDir = this.ConvertDirToVector3(pMsg.begindir);
        Vector3 targetPos = this.ConvertPosToVector3(pMsg.tarpos);
        Ientity entity = null;
        EntityManager.AllEntitys.TryGetValue(masterKey, out entity);
        SkillMoveConfig skillInfo = ConfigReader.GetSkillMoveConfig(pMsg.effectid);
        if (entity != null)
        {
            entity.RealEntity.HideTrail();
            entity.RealEntity.gameObject.transform.position = targetPos;
            entity.RealEntity.gameObject.transform.rotation = Quaternion.LookRotation(beginDir);
            entity.RealEntity.ShowTrail();
        }
        if (skillInfo != null)
        {
            string startPath = GameConstDefine.LoadGameSkillEffectPath + "release/" + skillInfo.effectStart;
            string endPath = GameConstDefine.LoadGameSkillEffectPath + "release/" + skillInfo.effectEnd;
            EffectManager.Instance.CreateNormalEffect(startPath, beginPos, beginDir);
            EffectManager.Instance.CreateNormalEffect(endPath, targetPos, beginDir);
            if (entity != null)
            {
                entity.RealEntity.PlayerAnimation(skillInfo.action);
            }
        }
        if (UIViewerPersonInfo.Instance != null)
        {
            Iplayer player = UIViewerPersonInfo.Instance.SetCurrClickPlayer;
            if (player == entity)
            {
                UIViewerPersonInfo.Instance.SetCurrCamearLockHead();
            }
        }
        EventCenter.Broadcast(EGameEvent.eGameEvent_HeroBackTown, (Iplayer)entity);
        return (Int32)EErrorCode.eNormal;
    }
    /// <summary>
    /// 强制位移
    /// </summary>
    /// <param name="pcMsg"></param>
    /// <returns></returns>
    //已换NotifySkillModelStartForceMoveStop
    //已换OnNetMsg_NotifySkillModelStartForceMoveStop
    Int32 OnNetMsg_NotifySkillModelStartForceMoveStop(Stream stream)
    {
        GSToGC.NotifySkillModelStartForceMoveStop pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sGUID;
        sGUID = pMsg.guid;
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
            Vector3 dir = entity.EntityFSMDirection;
            entity.EntityFSMChangedata(pos, dir);
            entity.GOSSI.sServerBeginPos = pos;
            entity.GOSSI.sServerSyncPos = pos;
            entity.GOSSI.sLocalSyncDir = dir;
            entity.GOSSI.fBeginTime = Time.realtimeSinceStartup;
            entity.GOSSI.fLastSyncSecond = Time.realtimeSinceStartup;
            if (Vector3.Distance(pos, entity.realObject.transform.position) >= 1f)
            {
                entity.realObject.transform.position = pos;
            }
            SkillMoveConfig skillInfo = ConfigReader.GetSkillMoveConfig(pMsg.effectid);
            if (skillInfo != null)
            {
                EffectManager.Instance.CreateNormalEffect(GameConstDefine.LoadGameSkillEffectPath + "release/" + skillInfo.effectEnd, entity.RealEntity.objPoint.gameObject);
            }
        }
        return (Int32)EErrorCode.eNormal;
    }


    /// <summary>
    /// 强制位移结束
    /// </summary>
    /// <param name="pcMsg"></param>
    /// <returns></returns>
    //已换NotifySkillModelStartForceMove
    //eMsgToGCFromGS_NotifySkillModelStartForceMove
    Int32 OnNetMsg_NotifySkillModelStartForceMove(Stream stream)
    {
        GSToGC.NotifySkillModelStartForceMove pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sGUID;
        sGUID = pMsg.guid;
        float speed = (float)pMsg.speed / 100;
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            Vector3 pos = this.ConvertPosToVector3(pMsg.pod);
            Vector3 dir = this.ConvertDirToVector3(pMsg.dir);
            entity.EntityFSMChangedata(pos, dir, speed);
            entity.GOSSI.sServerBeginPos = pos;
            entity.GOSSI.sServerSyncPos = pos;
            entity.GOSSI.sLocalSyncDir = dir;
            entity.GOSSI.sServerDir = dir;
            entity.GOSSI.fServerSpeed = speed;
            entity.GOSSI.fForceMoveSpeed = speed;
            entity.GOSSI.fBeginTime = Time.realtimeSinceStartup;
            entity.EntityFSMChangeDataOnForceMove(pMsg.effectid);
            entity.OnFSMStateChange(EntityForceMoveFSM.Instance);
            SkillMoveConfig skillInfo = ConfigReader.GetSkillMoveConfig(pMsg.effectid);
            if (skillInfo != null)
            {
                EffectManager.Instance.CreateNormalEffect(GameConstDefine.LoadGameSkillEffectPath + "release/" + skillInfo.effectStart, entity.RealEntity.objPoint.gameObject);
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifyBornObj(Stream stream)
    {
        GSToGC.NotifyBornObj pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        NpcConfigInfo info = ConfigReader.GetNpcInfo(pMsg.type);       //ToReview npcId是否就是type
        if (info != null)
        {
            List<float> probabilityList = GameMethod.ResolveToFloatList(info.n32Script1Rate);//获得概率集合
            for (int i = 0; i < probabilityList.Count; i++)
            {
                probabilityList[i] = (float)(probabilityList[i] - 90000) / 100f;
            }
            int index = GameMethod.RandProbablityIndex(probabilityList);//获得概率下标
            List<string> voiceList = GameMethod.ResolveToStrList(info.un32Script1);
            string name = voiceList[index];//获得概率下标对应的声音
            string path = AudioDefine.PATH_JUNGLE_MONSTER_BE_ATK_SOUND + name;
            //AudioClip clip = Resources.Load(path) as AudioClip;
            ResourceUnit unit = ResourcesManager.Instance.loadImmediate(path, ResourceType.ASSET);
            AudioClip clip = unit.Asset as AudioClip;
            AudioManager.Instance.PlayLongVoiceAudio(clip);
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg__NotifyBattleManagerChange()
    {
        if (GameStateManager.Instance.GetCurState().GetStateType() == GameStateType.GS_Room)
        {
            MsgInfoManager.Instance.ShowMsg((int)ERROR_TYPE.eT_ManagerChange);
        }

        return (Int32)EErrorCode.eNormal;
    }

    //已换LeaveBattleSuccess
    //eMsgToGCFromGS_NotifyLeaveBattleSuccess
    Int32 OnNetMsg_NotifyLeaveBattleSuccess()
    {
        PlayerManager.Instance.CleanAccount();

        EventCenter.SendEvent(new CEvent(EGameEvent.eGameEvent_RoomBack));

        return (Int32)EErrorCode.eNormal;
    }

    //已换NotifySummonLifeTime
    //eMsgToGCFromGS_NotifySummonLifeTime
    Int32 OnNetMsg_NotifySummonLifeTime(Stream stream)
    {
        GSToGC.NotifySummonLifeTime pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 objGuid;
        objGuid = pMsg.guid;
        float totalTime = pMsg.lifetime / 1000.0f;
        float remainTime = pMsg.resttime / 1000.0f;
        Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 dir = this.ConvertDirToVector3(pMsg.dir);
        Ientity entity;
        EntityManager.AllEntitys.TryGetValue(objGuid, out entity);
        if (entity != null)
        {
            entity.RealEntity.gameObject.transform.position = pos;
            entity.RealEntity.gameObject.transform.rotation = Quaternion.LookRotation(dir);
            entity.EntityOverplusRemainTime = remainTime;
            entity.EntityOverplusTotalTime = totalTime;
        }
        //设置进度条
        //原本为空
        return (Int32)EErrorCode.eNormal;
    }

    /************************************************************************/
    /*OnNetMsg_NotifySkillModelSummonEffect                                 */
    /************************************************************************/
    Int32 OnNetMsg_NotifySkillModelSummonEffect(Stream stream)
    {
        StartCoroutine(OnNetMsg_NotifySkillModelSummonEffectCoroutine(stream));

        return (Int32)EErrorCode.eNormal;
    }

    public IEnumerator OnNetMsg_NotifySkillModelSummonEffectCoroutine(Stream stream)
    {
        //解析消息
        GSToGC.SummonEffect pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            yield break;
        }

        //创建特效
        yield return 1;

        UInt64 skillowner;
        skillowner = pMsg.guid;
        UInt64 npcid;
        npcid = pMsg.guid;
        Ientity entitynpc = null;
        EntityManager.AllEntitys.TryGetValue(npcid, out entitynpc);
        SkillSummonConfig skillconfig = ConfigReader.GetSkillSummonConfig((int)pMsg.effectid);  //ToReview uint->int
        if (entitynpc != null && skillconfig != null)
        {
            BlGame.Effect.IEffect effect = BlGame.Effect.EffectManager.Instance.CreateNormalEffect(GameConstDefine.LoadGameSkillEffectPath + "release/" + skillconfig.effect, entitynpc.RealEntity.objPoint.gameObject);


            //创建声音
            yield return 1;

            string soundPath = "";
            soundPath = GameConstDefine.LoadGameSoundPath + skillconfig.sound;

            ResourceUnit objUnit = ResourcesManager.Instance.loadImmediate(soundPath, ResourceType.ASSET);

            if (objUnit.Asset != null)
            {
                AudioClip clip = objUnit.Asset as AudioClip;
                if (clip != null)
                {
                    AudioSource Audio = AudioManager.Instance.PlayEffectAudio(clip);
                    if (effect != null)
                    {
                        SceneSoundManager.Instance.addSound(Audio, effect.obj);
                    }
                }
            }
        }

    }


    /************************************************************************/
    /* OnNetMsg_NotifySkillModelBuf                                         */
    /************************************************************************/
    Int32 OnNetMsg_NotifySkillModelBuf(Stream stream)
    {
        StartCoroutine(OnNetMsg_NotifySkillModelBufCoroutine(stream));
        return (Int32)EErrorCode.eNormal;
    }

    public IEnumerator OnNetMsg_NotifySkillModelBufCoroutine(Stream stream)
    {
        //解析消息
        GSToGC.BuffEffect pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            yield break;
        }

        yield return 1;

        //创建特效
        UInt64 skillowner;
        skillowner = pMsg.guid;
        UInt64 skilltarget;
        skilltarget = pMsg.targuid;
        float rTime = pMsg.time / 1000.0f;
        Ientity target = null;
        EntityManager.AllEntitys.TryGetValue(skilltarget, out target);
        if (0 == pMsg.state)
        {
            BlGame.Skill.BuffManager.Instance.AddBuff(pMsg.uniqueid, pMsg.effectid, rTime, target);
            Ientity entity = null;
            EntityManager.AllEntitys.TryGetValue(skilltarget, out entity);
            BlGame.Effect.EffectManager.Instance.CreateBuffEffect(entity, pMsg.effectid, pMsg.uniqueid);    //ToReview uniqueid是否就是instid
        }
        else if (1 == pMsg.state)
        {
            BlGame.Skill.BuffManager.Instance.RemoveBuff(pMsg.uniqueid);
            BlGame.Effect.EffectManager.Instance.DestroyEffect(pMsg.uniqueid);
        }
    }


    /************************************************************************/
    /*OnNetMsg_NotifySkillModelLeading                                      */
    /************************************************************************/
    public Int32 OnNetMsg_NotifySkillModelLeading(Stream stream)
    {
        StartCoroutine(SkillModelLeadingCoroutine(stream));
        return (Int32)EErrorCode.eNormal;
    }

    public IEnumerator SkillModelLeadingCoroutine(Stream stream)
    {
        //解析消息
        GSToGC.NotifySkillModelLeading pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            yield break;
        }

        //创建特效
        yield return 1;

        UInt64 owner = pMsg.guid;
        UInt64 target = pMsg.targuid;
        Ientity ownerEntity;
        EntityManager.AllEntitys.TryGetValue(owner, out ownerEntity);
        SkillLeadingonfig skillconfig = ConfigReader.GetSkillLeadingConfig(pMsg.effectid);
        if (skillconfig != null && ownerEntity != null)
        {
            Iplayer player = PlayerManager.Instance.LocalPlayer;
            if (0 == pMsg.state)    //0:开始，1：结束，2：失败
            {
                if (false == BlGame.Effect.EffectManager.Instance.IsValid(pMsg.uniqueid))
                {
                    BlGame.Effect.EffectManager.Instance.CreateLeadingEffect(owner, (uint)pMsg.effectid, (uint)pMsg.uniqueid);
                }
                if (player != null && owner == player.GameObjGUID)
                {
                    ProgressBarInterface.startProgressBar(skillconfig.time);
                    //Debug.Log("pMsg.effectid" + pMsg.effectid);
                }
            }
            else
            {
                BlGame.Effect.EffectManager.Instance.DestroyEffect(pMsg.uniqueid);
                if (player != null && owner == player.GameObjGUID)
                {
                    ProgressBarInterface.hideProgressBar();
                }
            }
        }
    }


    //已换RangeEffectEnd
    //eMsgToGCFromGS_NotifySkillModelRangeEnd
    Int32 OnNetMsg_NotifySkillModelRangeEnd(Stream stream)
    {
        GSToGC.RangeEffectEnd pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        //ToReview guid没用上
        BlGame.Effect.EffectManager.Instance.DestroyEffect(pMsg.uniqueid);
        return (Int32)EErrorCode.eNormal;
    }

    /************************************************************************/
    /*OnNetMsg_NotifySkillModelRange                                         */
    /************************************************************************/
    Int32 OnNetMsg_NotifySkillModelRange(Stream stream)
    {
        StartCoroutine(OnNetMsg_NotifySkillModelRangeCoroutine(stream));
        return (Int32)EErrorCode.eNormal;
    }

    public IEnumerator OnNetMsg_NotifySkillModelRangeCoroutine(Stream stream)
    {
        //解析消息
        GSToGC.RangeEffect pMsg = null;
        if (!ProtoDes(out pMsg, stream))
        {
            yield break;
        }

        if (pMsg != null)
        {
            UInt64 owner = pMsg.guid;
            Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
            Vector3 dir = this.ConvertDirToVector3(pMsg.dir);

            //创建特效
            yield return 1;
            BlGame.Effect.EffectManager.Instance.CreateAreaEffect(owner, pMsg.effectid, pMsg.uniqueid, dir, pos);
        }
        else
        {
            Debug.LogError("msg is null in OnNetMsg_NotifySkillModelRangeCoroutine");
        }
    }


    /************************************************************************/
    /*OnNetMsg_NotifySkillModelEmit                                         */
    /************************************************************************/
    Int32 OnNetMsg_NotifySkillModelEmit(Stream stream)
    {
        StartCoroutine(OnNetMsg_NotifySkillModelEmitCoroutine(stream));       
        return (Int32)EErrorCode.eNormal;  
    }

    public IEnumerator OnNetMsg_NotifySkillModelEmitCoroutine(Stream stream)
    {
        //解析消息
        GSToGC.EmitSkill pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            yield break;
        }

        UInt64 skillPlayerID = pMsg.guid;
        UInt64 skillTargetID = pMsg.targuid;
        Vector3 pos = this.ConvertPosToVector3(pMsg.tarpos);
        Vector3 dir = this.ConvertDirToVector3(pMsg.dir);

        //创建特效
        yield return 1;
        FlyEffect effect = BlGame.Effect.EffectManager.Instance.CreateFlyEffect(skillPlayerID, skillTargetID, pMsg.effectid, (uint)pMsg.uniqueid, pos, dir, pMsg.ifAbsorbSkill);
    }



    Int32 OnNetMsg_NotifySkillModelEmitDestroy(Stream stream)
    {
        GSToGC.DestroyEmitEffect pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        IEffect effect = EffectManager.Instance.GetEffect(pMsg.uniqueid);
        if (effect != null)
        {
            if (effect.mType == IEffect.ESkillEffectType.eET_FlyEffect)
            {
                FlyEffect flyEffect = effect as FlyEffect;

                //拖拽类型
                if (flyEffect.emitType == 8)
                {
                    flyEffect.DragRibbonBack();
                }
                else
                    EffectManager.Instance.DestroyEffect(flyEffect);
            }
            else
            {
                EffectManager.Instance.DestroyEffect(effect);
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    /************************************************************************/
    /* OnNetMsg_NotifySkillModelHitTarget                                   */
    /************************************************************************/
    Int32 OnNetMsg_NotifySkillModelHitTarget(Stream stream)
    {
        StartCoroutine(OnNetMsg_NotifySkillModelHitTargetCoroutine(stream));
        return (Int32)EErrorCode.eNormal;
    }

    public IEnumerator OnNetMsg_NotifySkillModelHitTargetCoroutine(Stream stream)
    {
        //解析消息
        GSToGC.HitTar pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            yield break;
        }

        //创建特效
        UInt64 ownerID;
        ownerID = pMsg.guid;
        UInt64 targetID;
        targetID = pMsg.targuid;

        EventCenter.Broadcast<UInt64, uint, UInt64>(EGameEvent.eGameEvent_BroadcastBeAtk, ownerID, pMsg.effectid, targetID);
        yield return 1;
        BlGame.Effect.EffectManager.Instance.CreateBeAttackEffect(ownerID, targetID, pMsg.effectid);
    }



    //广播祭坛头像
    public enum eAltarBorn
    {
        eAltarBornNPCType_Add,
        eAltarBornNPCType_Del,
    }
    Int32 OnNetMsg_NotifyAltarBSIco(Stream stream)
    {
        GSToGC.NotifyAltarBSIco pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        // Int64 altrGuid = (Int64)pMsg.guid;
        Int32 altrId = (int)pMsg.altarid;
        Int32 soderType = (int)pMsg.objtype;
        Int32 optype = (int)pMsg.optype;
        if ((eAltarBorn)optype == eAltarBorn.eAltarBornNPCType_Add)
        {
            AltarData.Instance.AddAltarDic(altrId, soderType);
            if (AltarManager.Instance != null)
                AltarManager.Instance.ShowAltarHead(altrId, soderType);
        }
        else
        {
            AltarData.Instance.DelAltarDic(altrId);
            if (AltarManager.Instance != null)
                AltarManager.Instance.DelAltarHead(altrId, soderType);
        }
        return (Int32)EErrorCode.eNormal;
    }
    Int32 OnNotifyHeroDisplacementInfo(CMsg pcMsg)
    {
        UInt64 sGUID = pcMsg.GetGUID();
        float dirX = pcMsg.GetFloat() / 100;
        float dirY = pcMsg.GetFloat() / 100;
        float dirZ = pcMsg.GetFloat() / 100;
        float posX = pcMsg.GetFloat() / 100;
        float posY = pcMsg.GetFloat() / 100;
        float posZ = pcMsg.GetFloat() / 100;
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            if (entity.realObject != null)
            {
                entity.realObject.transform.position = new Vector3(posX, posY, posZ);
                entity.realObject.transform.rotation = Quaternion.LookRotation(new Vector3(dirX, dirY, dirZ));
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    #region Skill

    //已换
    Int32 OnNetMsg_BroadCurBattleResult(Stream stream)
    {
        Iplayer player = PlayerManager.Instance.LocalPlayer;
        if (player == null)
        {
            return (Int32)EErrorCode.eNormal;
        }
        GSToGC.BroadcastBatteleRes pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        ScoreCtrl.Instance.GetSettlementList().Clear();
        foreach (GSToGC.BroadcastBatteleRes.ResInfo info in pMsg.resinfo)
        {
            UInt64 objGUID;
            objGUID = info.objguid;
            ScoreCtrl.Instance.SetSettlementInfo(objGUID, info.heroid, info.nickname, info.killtimes,
                info.deadtimes, info.asstimes, info.curlevel, info.totalcp / 1000, info.lasthit, (EntityCampType)info.camgpid);
        }
        bool isWinCamp = pMsg.ifwin;
        if (isWinCamp)
        {
            PlayerManager.Instance.LocalPlayer.StateSituation = SITUATION.WIN;
        }
        else
        {
            PlayerManager.Instance.LocalPlayer.StateSituation = SITUATION.LOSE;
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_BroadCurBattlePersonalResult(Stream stream)
    {
        Iplayer player = PlayerManager.Instance.LocalPlayer;
        if (player == null)
        {
            return (Int32)EErrorCode.eNormal;
        }
        GSToGC.BroadcastBattelePersonalRes pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        ScoreCtrl.Instance.SetScoreInfo(pMsg.got_gold, pMsg.old_exp, pMsg.got_exp, (short)pMsg.old_lv, (short)pMsg.cur_lv, pMsg.cur_exp);
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_BroadcastBattleHeroInfo(Stream stream)
    {
        GSToGC.BroadcastBattleHeroInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (GSToGC.BroadcastBattleHeroInfo.HeroInfo info in pMsg.heroinfo)
        {
            UInt64 MsGUID = info.masterguid;
            UInt64 sGUID = info.guid;
            string nickName = info.nickname;
            int level = info.level;
            int heroKills = info.kills;
            int deadTimes = info.deadtimes;
            Int32 campID = info.campid;
            Int32 headID = info.headid;
            Int32 hp = info.hp;
            Int32 mp = info.mp;
            EntityCampType Type = GameMethod.GetEntityCamp(campID);
            BattleingData.Instance.AddInitPlayer(sGUID, nickName, heroKills, deadTimes, info.assist, level, 0, Type, info.heroid);
            foreach (GSToGC.BroadcastBattleHeroInfo.BaseGoodsInfo goods in pMsg.goodsinfo)
            {
                BattleingData.Instance.AddPlayer(sGUID, 0, BattleDataType.Goods, goods.index, goods.id);
            }
            Iplayer player;
            if (!PlayerManager.Instance.AccountDic.TryGetValue(MsGUID, out player))
            {
                player = (Iplayer)PlayerManager.Instance.HandleCreateEntity(MsGUID, EntityCampType.CampTypeNull);
                player.BattleData.Level = (uint)level;
                player.BattleData.HeadId = (uint)headID;
                player.BattleData.Hp = (uint)hp;
                player.BattleData.Mp = (uint)mp;
            }
            if (UIGameBattleInfo.Instance != null && PlayerManager.Instance.LocalAccount.ObType == ObPlayerOrPlayer.PlayerType)
            {
                UIGameBattleInfo.Instance.SetInfoInit(sGUID, nickName, level, heroKills, deadTimes, Type);
            }
            else
            {
                if (UIViewerBattleInfo.Instance != null)
                {
                    UIViewerBattleInfo.Instance.SetInfoInit(sGUID, nickName, level, heroKills, deadTimes, Type, campID);
                }

            }
            PlayerPersonData.Instance.SetPersonInfo(player, campID);
            EventCenter.Broadcast(EGameEvent.eGameEvent_PersonInitInfo, player);
            EventCenter.Broadcast(EGameEvent.eGameEvent_SkillInfo, player);
            EventCenter.Broadcast(EGameEvent.eGameEvent_LocalPlayerCp);
            EventCenter.Broadcast(EGameEvent.eGameEvent_NotifyChangeDeaths);
            EventCenter.Broadcast(EGameEvent.eGameEvent_NotifyChangeKills);
            EventCenter.Broadcast(EGameEvent.eGameEvent_HeroInfoChange, player);
        }
        if (UIViewerPersonInfo.Instance != null)
            UIViewerPersonInfo.Instance.SetInitCamearHead();
        return (Int32)EErrorCode.eNormal;
    }

    //已换AbsorbBegin
    Int32 OnNetMsg_NotifyAbsorbBegin(Stream stream)
    {
        GSToGC.AbsorbBegin pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sceneGuid;
        sceneGuid = pMsg.guid;
        UInt64 targetGuid;
        targetGuid = pMsg.monsterguid;
        Ientity entity;
        Ientity target;
        EntityManager.AllEntitys.TryGetValue(sceneGuid, out entity);
        EntityManager.AllEntitys.TryGetValue(targetGuid, out target);
        Iplayer player = entity as Iplayer;
        if (player != null && target != null)
        {
            AbsorbEffect.createAbsorbEffect(player, target.RealEntity.objAttackPoint.gameObject, player.RealEntity.objAttackPoint.gameObject);
            if (player.absorbSound != null)
            {
                player.absorbSound.Stop();
                player.absorbSound = null;
            }
            string soundPath = "";
            soundPath = GameConstDefine.LoadGameSoundPath + "Txifu";
            ResourceUnit objUnit = ResourcesManager.Instance.loadImmediate(soundPath, ResourceType.ASSET);
            if (objUnit.Asset != null)
            {
                AudioClip clip = objUnit.Asset as AudioClip;
                if (clip != null)
                {
                    AudioSource Audio = AudioManager.Instance.PlayLongVoiceAudio(clip);
                    SceneSoundManager.Instance.addSound(Audio, player.RealEntity.gameObject);
                    player.absorbSound = Audio;
                    AudioManager.Instance.ChangeAudioVolume(Audio, 1.0f);
                }
            }
            if (player == PlayerManager.Instance.LocalPlayer)
            {
                ProgressBarInterface.startProgressBar(ProgressBarInterface.BarType.BarAbsorb);
                PlayerManager.Instance.LocalPlayer.IsAbsobing = true;
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    enum EAbsorbResult
    {
        eAbsorbResult_Failed,
        eAbsorbResult_Success
    }

    //已换AbsorbRes
    //eMsgToGCFromGS_NotifyAbsorbMonsterResult
    Int32 OnNetMsg_NotifyAbsorbMonsterResult(Stream stream)
    {
        GSToGC.AbsorbRes pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sceneGuid;
        sceneGuid = pMsg.guid;
        UInt32 newMonsterID1 = (UInt32)pMsg.absorb1;
        UInt32 newMonsterID2 = (UInt32)pMsg.absorb2;
        Ientity abosorbPlayer;
        EntityManager.AllEntitys.TryGetValue(sceneGuid, out abosorbPlayer);
        if (null == abosorbPlayer)
        {
            return 0;
        }
        if (null != abosorbPlayer.absorbSound)
        {
            abosorbPlayer.absorbSound.Stop();
            SceneSoundManager.Instance.remove(abosorbPlayer.absorbSound);
            abosorbPlayer.absorbSound = null;
        }
        PlayerSkillData.Instance.SetPlayerAbsSkillInfo((Iplayer)abosorbPlayer, 4, (int)newMonsterID1, 5, (int)newMonsterID2);
        Iselfplayer curPlayer = PlayerManager.Instance.LocalPlayer;
        if (null == curPlayer)
        {
            EventCenter.Broadcast(EGameEvent.eGameEvent_AbsSkillInfo, (Iplayer)abosorbPlayer);
            return 0;
        }
        if (curPlayer.GameObjGUID == sceneGuid)
        {
            curPlayer.IsAbsobing = false;
            if (pMsg.res)
            {
                UInt32 un32OldMonsterID1 = (UInt32)curPlayer.AbsorbMonsterType[0];
                if (un32OldMonsterID1 != newMonsterID1)
                {
                    curPlayer.SetAbsorbMonster((int)newMonsterID1, 0);
                    EventCenter.Broadcast(EGameEvent.eGameEvent_GuideAbsorbTask, (int)newMonsterID1);
                }
                UInt32 un32OldMonsterID2 = (UInt32)curPlayer.AbsorbMonsterType[1];
                if (un32OldMonsterID2 != newMonsterID2)
                {
                    curPlayer.SetAbsorbMonster((int)newMonsterID2, 1);
                    EventCenter.Broadcast(EGameEvent.eGameEvent_GuideAbsorbTask, (int)newMonsterID2);
                }
                EventCenter.Broadcast(EGameEvent.eGameEvent_ResetAbsHead, (int)newMonsterID1, (int)newMonsterID2);
            }
            if (ProgressBarInterface.barType == ProgressBarInterface.BarType.BarAbsorb)
            {
                ProgressBarInterface.hideProgressBar();
            }
            if (curPlayer.AbsorbProgressEffect != null)
            {
                DestroyImmediate(curPlayer.AbsorbProgressEffect);
                curPlayer.AbsorbProgressEffect = null;
            }
        }
        else
        {
            Iplayer otherplayer = abosorbPlayer as Iplayer;
            if (otherplayer != null)
            {
                if (otherplayer.AbsorbProgressEffect != null)
                {
                    DestroyImmediate(otherplayer.AbsorbProgressEffect);
                    otherplayer.AbsorbProgressEffect = null;
                }
                if (pMsg.res)
                {
                    bool isRemove = (newMonsterID1 == 0) ? true : false;
                    otherplayer.SetAbsorbEffect(0, isRemove);

                    isRemove = (newMonsterID2 == 0) ? true : false;
                    otherplayer.SetAbsorbEffect(1, isRemove);
                }
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifyBuyRebornSuccess(Stream stream)
    {
        GSToGC.RebornSuccess pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sguid;
        sguid = pMsg.guid;
        DeathCtrl.Instance.Exit();
        Ientity entity = null;
        EntityManager.AllEntitys.TryGetValue(sguid, out entity);
        PlayerPersonData.Instance.SetDeathTime((Iplayer)entity, 0);
        EventCenter.Broadcast(EGameEvent.eGameEvent_HeroDeathTime, (Iplayer)entity);
        return (Int32)EErrorCode.eNormal;
    }

    /// <summary>
    /// 复活事件
    /// </summary>
    /// <param name="pcMsg"></param>
    /// <returns></returns>
    //已换
    Int32 OnNetMsg_NotifyHeroRebornTimes(Stream stream)
    {
        GSToGC.RebornTimes pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sguid;
        sguid = pMsg.masterguid;
        UInt64 BeforTime = (ulong)pMsg.sendtimes;   //ToReview sendTimes是否就是Beforetime，强制类型转换long->ulong             
        Int32 RebornTime = pMsg.reborn_time;
        Ientity entity = null;
        if (EntityManager.AllEntitys.TryGetValue(sguid, out entity))
        {

        }
        Iplayer player = PlayerManager.Instance.LocalPlayer;
        if (player != null)
        {
            DeathCtrl.Instance.SetTime((float)CTools.GetClientUTCMillisec() - BeforTime, (float)pMsg.remain_times / 1000f, pMsg.gold, RebornTime, true);//Num是否就是剩余次数remain_times    
            DeathCtrl.Instance.Enter();
        }
        PlayerPersonData.Instance.SetDeathTime((Iplayer)entity, (int)(pMsg.remain_times / 1000f));
        EventCenter.Broadcast(EGameEvent.eGameEvent_HeroDeathTime, (Iplayer)entity);
        return (Int32)EErrorCode.eNormal;
    }

    //击杀数
    //已换HeroKills
    //eMsgToGCFromGS_NotifyHeroKills
    Int32 OnNetMsg_NotifyHeroKills(Stream stream)
    {
        GSToGC.HeroKills pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sguid;
        sguid = pMsg.guid;
        //if (UIGameRecord.Instance != null)
        //{
        //    UIGameRecord.Instance.SetKill(sguid, pMsg.kills);
        //}

        BattleingData.Instance.AddPlayer(sguid, pMsg.kills, BattleDataType.Kills);
        if (UIGameBattleInfo.Instance != null)
        {
            UIGameBattleInfo.Instance.SetKillInfo(sguid, pMsg.kills);
        }
        if (UIViewerBattleInfo.Instance != null)
        {
            UIViewerBattleInfo.Instance.SetKillInfo(sguid, pMsg.kills);
        }
        EventCenter.Broadcast(EGameEvent.eGameEvent_NotifyChangeKills);
        //EventCenter.Broadcast(EGameEvent.eGameEvent_AllPlayerKills);
        // MsgInfoManager.Instance.ShowKill(MsgInfoManager.eKillMsgType.eKillNormal, is_FB, is_All, "", "");    //原有注释
        return (Int32)EErrorCode.eNormal;
    }

    //死亡数 
    //已换CurDeadTimes
    //eMsgToGCFromGS_NotifyCurDeadTimes
    Int32 OnNetMsg_NotifyCurDeadTimes(Stream stream)
    {
        GSToGC.CurDeadTimes pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        if (GameStateManager.Instance.GetCurState().GetStateType() != GameStateType.GS_Play)
            return (Int32)EErrorCode.eNormal;

        UInt64 targetID;
        targetID = pMsg.objguid;
        UInt64 killID;
        killID = pMsg.reasonheroguid;

        BattleingData.Instance.AddPlayer(targetID, pMsg.deadtimes, BattleDataType.Deaths);
        MsgInfoManager.Instance.SetKillDeathPlayer((byte)pMsg.herostate, pMsg.killer_camp, targetID, killID, pMsg.ifAced, (EHeroKillTitle)pMsg.herotitle);       //ToReview int->byte
        EventCenter.Broadcast(EGameEvent.eGameEvent_NotifyChangeDeaths);
        EventCenter.Broadcast(EGameEvent.eGameEvent_AllPlayerDeaths);
        //ToReview killer_camp没用上
        return (Int32)EErrorCode.eNormal;
    }

    //已换CurCP
    //eMsgToGCFromGS_NotifyCurCP
    Int32 OnNetMsg_NotifyCurCP(Stream stream)
    {
        GSToGC.CurCP pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 targetID;
        targetID = pMsg.targetguid;
        if (PlayerManager.Instance.LocalPlayer != null && PlayerManager.Instance.LocalAccount.ObType == ObPlayerOrPlayer.PlayerType)
        {
            CEvent eve = new CEvent(EGameEvent.eGameEvent_NotifyChangeCp);
            eve.AddParam("Cp", pMsg.person_cp / 1000);
            eve.AddParam("TeamCp", pMsg.team_cp / 1000);
            EventCenter.SendEvent(eve);

            PlayerManager.Instance.LocalPlayer.SetCp(pMsg.person_cp / 1000);
            PlayerManager.Instance.LocalPlayer.SetTeamCp(pMsg.team_cp / 1000);
            //if (UIGameRecord.Instance != null)
            //    UIGameRecord.Instance.SetCp(pMsg.person_cp / 1000);
        }
        else
        {
            if (UIViewerBattleInfo.Instance != null)
            {
                UIViewerBattleInfo.Instance.SetCpInfo(targetID, pMsg.person_cp / 1000);
            }
        }
        BattleingData.Instance.AddPlayer(targetID, pMsg.person_cp / 1000, BattleDataType.Cp);
        EventCenter.Broadcast(EGameEvent.eGameEvent_LocalPlayerCp);
        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifyCPChange(Stream stream)
    {
        GSToGC.PersonCPChange pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 targetID;
        targetID = pMsg.guid;
        if (CPChanggeManager.Instance != null && PlayerManager.Instance.LocalPlayer!=null)
        {
            CPChanggeManager.Instance.CreateCPAdd(pMsg.cp / 1000);
            EventCenter.Broadcast(EGameEvent.eGameEvent_PlayerGetCp);
        }
        EventCenter.Broadcast(EGameEvent.eGameEvent_LocalPlayerCp);
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMst_BroadBuildingDestroyByWho(Stream stream)
    {
        GSToGC.BroadcastBuildingDestory pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        if (GameStateManager.Instance.GetCurState().GetStateType() != GameStateType.GS_Play)
            return (Int32)EErrorCode.eNormal;

        UInt64 targetID = pMsg.buildingguid;
        if (!EntityManager.AllEntitys.ContainsKey(targetID))
        {
            Debug.LogError("null entity!");
            return 0;
        }

        Ientity targetEntity = EntityManager.AllEntitys[targetID];

        UInt64 ps_kill = pMsg.killer_guid;
        if (!EntityManager.AllEntitys.ContainsKey(ps_kill))
        {
            Debug.LogError("null entity!");
            return 0;
        }

        Ientity psKillEntity = EntityManager.AllEntitys[ps_kill];

        Int32 killID = pMsg.killer_camp;
        EntityCampType Type = (EntityCampType)killID;
        string npcname = "";
        string readXml = "";
        string killname = "";
        if (killID > 0)
        {
            if (killID % 2 == 0)
            {
                Type = EntityCampType.CampTypeB;
            }
            else
            {
                Type = EntityCampType.CampTypeA;
            }
        }
        Iplayer player = PlayerManager.Instance.LocalPlayer;
        killname = MsgInfoManager.Instance.GetNameGame(ps_kill);
        if (killname == null)
        {
            NpcConfigInfo info;
            ConfigReader.NpcXmlInfoDict.TryGetValue(killID, out info);
            if (info != null)
            {
                killname = info.NpcName;
            }
            else
            {
                NpcConfigInfo npcinfo = ConfigReader.GetNpcInfo((int)psKillEntity.ObjTypeID);
                if (null != npcinfo && killname == null)
                {
                    if (Type == EntityCampType.CampTypeA)
                        killname = "精灵势力";
                    else
                        killname = "亡灵势力";
                }
            }
        }
        if (targetEntity.IfNPC())
        {
            int id = (int)targetEntity.ObjTypeID;
            NpcConfigInfo npcinfo = ConfigReader.GetNpcInfo(id);
            if (null == npcinfo)
            {
                return (Int32)EErrorCode.eEC_NullPointer;
            }
            if (npcinfo.ENPCCateChild == (int)ENPCCateChild.eNPCChild_BUILD_Tower)
                npcname = "箭塔";
            else if (npcinfo.ENPCCateChild == (int)ENPCCateChild.eNPCChild_BUILD_Altar)
                npcname = "祭坛";
            else if (npcinfo.ENPCCateChild == (int)ENPCCateChild.eNPCChild_BUILD_Base)
                npcname = "主基地";
            if (player != null && Type == player.EntityCamp)
            {
                readXml = ConfigReader.GetMsgInfo(10005).content;
            }
            else
            {
                readXml = ConfigReader.GetMsgInfo(10006).content;
            }
        }

        if (killname != "")
        {
            MsgInfoManager.Instance.SetKills(MsgInfoManager.eKillMsgType.eKillBuild, false, killname, npcname, readXml);
        }

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMst_NotifyUserGameInfo(Stream stream)
    {
        GSToGC.UserGameInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        //senlin
        //UILobby.Instance.SetGameLobbyBtn(OptionLobby.PERSONALINFO);
        PresonInfoCtrl.Instance.SetCurrentDate((int)pMsg.headid, pMsg.nickname, pMsg.level, pMsg.upgradeexp, pMsg.curexp, pMsg.totalgameinns, pMsg.totalwintimes, pMsg.herokills,
                pMsg.destorybuildings, pMsg.deadtimes, pMsg.total_achnum, pMsg.achnum, pMsg.assistnum, pMsg.vipscore, (ulong)pMsg.exp_adtime, (ulong)pMsg.gold_addtime);
        PresonInfoCtrl.Instance.Enter();
        //EventCenter.Broadcast(EGameEvent.eGameEvent_PersonInitInfo);
        return (Int32)EErrorCode.eNormal;
    }

    //NotifyBuffRemove已取消
    Int32 OnNetMsg_NotifyBuffRemove(CMsg pcMsg)
    {
        UInt64 targetID = pcMsg.GetGUID();
        UInt32 buffID = pcMsg.GetUInt32();
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(targetID, out entity))
        {
            BlGame.Effect.EffectManager.Instance.DestroyEffect(buffID);
            if (entity.GameObjGUID == PlayerManager.Instance.LocalPlayer.GameObjGUID)
            {
                BlGame.Skill.BuffManager.Instance.RemoveBuff(buffID);
            }
        }
        return (Int32)EErrorCode.eNormal;
    }


    //已换NotifySkillInfo
    //eMsgToGCFromGS_NotifySkillInfo
    Int32 OnNetMsg_NotifySkillInfo(Stream stream)
    {
        GSToGC.NotifySkillInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 targetID;
        targetID = pMsg.guid;
        float timeInSecond = pMsg.time / 1000.0f;   //状态持续的时间毫秒数
        float timeMax = pMsg.cooldown / 1000.0f;
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(targetID, out entity))
        {
            if (PlayerManager.Instance.LocalAccount.ObType == ObPlayerOrPlayer.PlayerType)
            {
                entity.OnSkillInfoChange(pMsg.skillid, timeInSecond, timeMax, pMsg.skillslot);    //ToReview enum->another enum //0,空闲 1，准备 2施放 3冷却
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换GOSkillCD
    //eMsgToGCFromGS_NotifyGameObjectSkillCD
    Int32 OnNetMsg_NotifyGameObjectSkillCD(Stream stream)
    {
        GSToGC.GOSkillCD pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sguid;
        sguid = pMsg.guid;
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sguid, out entity))
        {
            PlayerSkillData.Instance.SetPlayerCdTime((Iplayer)entity, pMsg.skillid, pMsg.time / 1000.0f);
        }
        EventCenter.Broadcast(EGameEvent.eGameEvent_SkillCDInfo, (Iplayer)entity);
        return (Int32)EErrorCode.eNormal;
    }
    /// <summary>
    /// Notify 物品信息
    /// </summary>
    /// <param name="pcMsg"></param>
    /// <returns></returns>
    //已换NotifyGoodsInfo
    //eMsgToGCFromGS_NotifyGoodsInf
    Int32 OnNetMsg_NotifyGoodsInfo(Stream stream)
    {
        JxBlGame.Instance.IsInitialize = false;

        GSToGC.NotifyGoodsInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 targetID;
        targetID = pMsg.guid;
        foreach (GSToGC.NotifyGoodsInfo.GoodsInfo info in pMsg.info)
        {
            int n32RemainCDTime = info.statetime;
            if (n32RemainCDTime < 0)
            {
                n32RemainCDTime = 0;
            }
            float totTime = 0.0f;
            if (true == ConfigReader.ItemXmlInfoDict.ContainsKey(info.tyepid))
            {
                totTime = ConfigReader.ItemXmlInfoDict[info.tyepid].un32CdTime;
            }
            float second = (float)n32RemainCDTime / 1000.0f;
            if (PlayerManager.Instance.LocalAccount.ObType == ObPlayerOrPlayer.PlayerType)
            {
                PlayerManager.Instance.LocalPlayer.AddUserGameItems(info.pos, info.tyepid, info.num, second);
            }
            Ientity entity;
            if (EntityManager.AllEntitys.TryGetValue(targetID, out entity))
            {
                PackageData.Instance.SetPackageData((Iplayer)entity, info.pos, info.tyepid, info.num, totTime, second);
                EventCenter.Broadcast(EGameEvent.eGameEvent_PackageBuyInfo, (Iplayer)entity);
            }
            EventCenter.Broadcast(EGameEvent.eGameEvent_UpdateUserGameItems);
            if (info.ifComposed)
            {
                ResourceUnit unit = ResourcesManager.Instance.loadImmediate(AudioDefine.ITEMCOMBINESOUNDEFFECT, ResourceType.ASSET);
                AudioClip clip = unit.Asset as AudioClip;
                AudioSource source = AudioManager.Instance.PlayEffectAudio(clip);
            }
            BattleingData.Instance.AddPlayer(targetID, info.num, BattleDataType.Goods, info.pos, info.tyepid);
            //EventCenter.Broadcast(EGameEvent.eGameEvent_AllPlayerGoods);
            /*
              //talkingdata
              #region  talkingdata
              CEvent eve = new CEvent(EGameEvent.eGameEvent_TalkgameAction);
              eve.AddParam("type", EActionType.eA_Battle_Hero_BuyGoods);
              eve.AddParam("p1", info.tyepid);
              eve.AddParam("p2", info.num);
              eve.AddParam("p3", System.DateTime.Now.ToString("yyyyMMddHHmmss"));
              EventCenter.SendEvent(eve);
              #endregion  
              */
        }
        //改为在GSToGC.BattleSpanTime消息中同步时间
        //if (pMsg.playtime > 0)
        //{
        //    EventCenter.Broadcast<long>(EGameEvent.eGameEvent_GameStartTime, pMsg.playtime);
        //    GameTimeData.Instance.UpdatePlayTime(pMsg.playtime);
        //}
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyGameObjectDeadState(Stream stream)
    {
        GSToGC.DeadState pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 deadID;
        deadID = pMsg.objguid;
        Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 dir = this.ConvertDirToVector3(pMsg.dir);
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(deadID, out entity))
        {
            pos.y = entity.realObject.transform.position.y;
            entity.deadSpot = pMsg.spot;
            entity.EntityFSMChangeDataOnDead(pos, dir);

            //主基地延时处理
            if (entity.NPCCateChild != ENPCCateChild.eNPCChild_BUILD_Base)
            {
                entity.OnFSMStateChange(EntityDeadFSM.Instance);
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换SkillEntityInfo
    //eMsgToGCFromGS_NotifySkillUnitInfo->eMsgToGCFromGS_NotifySkillEntityInfo
    Int32 OnNetMsg_NotifySkillUnitInfo(Stream stream)
    {
        //ToReview 函数体是空的?
        return (Int32)EErrorCode.eNormal;
    }

    //已换SkillHitTar
    //eMsgToGCFromGS_NotifySkillHitTarget
    Int32 OnNetMsg_NotifySkillHitTarget(Stream Stream)
    {
        //ToReview 函数体是空的？
        return (Int32)EErrorCode.eNormal;
    }

    //已换HPChange
    //eMsgToGCFromGS_NotifyHPChange
    Int32 OnNetMsg_NotifyHPChange(Stream stream)
    {
        GSToGC.HPChange pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sGUID;
        sGUID = pMsg.guid;
        int crticalHp = pMsg.hp;

        // Debug.LogError("---curt hp:" + pMsg.hp);

        Ientity entity;
        Vector3 posInWorld = Vector3.zero;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            posInWorld = entity.RealEntity.objAttackPoint.transform.position + new Vector3(0.0f, 2.0f, 0.0f);
            if (pMsg.reason != GSToGC.HPMPChangeReason.SkillHurt)
            {
                entity.SetJungleMonsterBeAtkVoice();
            }
            //先计算伤血数值再更新
            entity.UpdateHpChange((byte)pMsg.reason, (float)crticalHp);
            if (entity is Iplayer)
            {
                EventCenter.Broadcast(EGameEvent.eGameEvent_HeroHpChange, (Iplayer)entity);
            }
            GamePlayCtrl.Instance.HpChange(entity);
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyMPChange(Stream stream)
    {
        GSToGC.MpChange pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 id;
        id = pMsg.guid;
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(id, out entity))
        {
            if (pMsg.reason != 0)           //ToReview enum->byte
            {
                //以下两行以前就是被注释掉的
                //BlGame.Effect.EffectManager.Instance.CreateLabelEffect("" + (entity.Mp - CurMP), BlGame.Effect.LabelJumpEffect.LabelJumpType.LJT_MP, posInWorld);
                //BlGame.Effect.EffectManager.Instance.CreateLabelEffect("" + (CurMP - entity.Mp), BlGame.Effect.LabelJumpEffect.LabelJumpType.LJT_MP, posInWorld);
            }
            entity.SetMp((float)pMsg.mp);
            if (entity is Iplayer)
            {
                XueTiaoPlayer xueTiaoPlayer = (XueTiaoPlayer)entity.XueTiao;
                xueTiaoPlayer.UpdateMp();
                EventCenter.Broadcast(EGameEvent.eGameEvent_HeroMpChange, (Iplayer)entity);
            }
            GamePlayCtrl.Instance.MpChange(entity);
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyPrepareSkill(Stream stream)
    {
        GSToGC.PrepareSkillState pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 dir = this.ConvertDirToVector3(pMsg.dir);
        dir.y = 0.0f;
        UInt64 sGUID;
        sGUID = pMsg.objguid;
        UInt64 targetID;
        targetID = pMsg.targuid;
        //ToReview speed没用上
        Ientity targetEntity;
        EntityManager.AllEntitys.TryGetValue(targetID, out targetEntity);
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            pos.y = entity.realObject.transform.position.y;
            entity.EntityFSMChangeDataOnPrepareSkill(pos, dir, pMsg.skillid, targetEntity);
            entity.OnFSMStateChange(EntitySingFSM.Instance);
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyReleaseSkill(Stream stream)
    {
        GSToGC.ReleasingSkillState pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 dir = this.ConvertDirToVector3(pMsg.dir);
        dir.y = 0.0f;
        UInt64 targetID;
        targetID = pMsg.targuid;
        UInt64 sGUID;
        sGUID = pMsg.objguid;
        Ientity target;
        EntityManager.AllEntitys.TryGetValue(targetID, out target);
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            pos.y = entity.realObject.transform.position.y;
            entity.EntityFSMChangeDataOnPrepareSkill(pos, dir, pMsg.skillid, target);
            entity.OnFSMStateChange(EntityReleaseSkillFSM.Instance);
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyLeadingSkill(Stream stream)
    {
        GSToGC.UsingSkillState pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 dir = this.ConvertDirToVector3(pMsg.dir);
        dir.y = 0.0f;
        UInt64 targetID;
        targetID = pMsg.targuid;
        UInt64 sGUID;
        sGUID = pMsg.objguid;
        Ientity target;
        EntityManager.AllEntitys.TryGetValue(targetID, out target);
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            pos.y = entity.realObject.transform.position.y;
            entity.EntityFSMChangeDataOnPrepareSkill(pos, dir, pMsg.skillid, target);
            entity.OnFSMStateChange(EntityLeadingFSM.Instance);
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyLastingSkill(Stream stream)
    {
        GSToGC.LastingSkillState pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 dir = this.ConvertDirToVector3(pMsg.dir);
        dir.y = 0.0f;
        UInt64 targetID;
        targetID = pMsg.targuid;
        UInt64 sGUID;
        sGUID = pMsg.objguid;
        Ientity target;
        EntityManager.AllEntitys.TryGetValue(targetID, out target);
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            pos.y = entity.realObject.transform.position.y;
            entity.EntityFSMChangeDataOnPrepareSkill(pos, dir, pMsg.skillid, target);
            entity.OnFSMStateChange(EntityLastingFSM.Instance);
        }
        return (Int32)EErrorCode.eNormal;
    }


    #endregion




    #region Entity State Change
    //已换BattleLoadingState
    //eMsgToGCFromGS_NotifyBattleLoadingState
    //ToReview：名称不对应，且函数体本身就被注释掉了
    Int32 OnNetMsg_NotifyBattleStart(Stream stream)
    {
        //		UInt32 HeroInfoNum = pcMsg.GetUInt32 ();
        //		for (int num = 0; num < HeroInfoNum; num++) {
        //			UInt64 sGUID = pcMsg.GetGUID();	
        //			Vector3 pos = new Vector3(pcMsg.GetFloat(), pcMsg.GetFloat(), pcMsg.GetFloat());
        //			Vector3 dir = new Vector3(pcMsg.GetFloat(), pcMsg.GetFloat(), pcMsg.GetFloat());
        //			PlayerManager.Instance.CreateEntityModel(sGUID, dir, pos,"Jinglingnv_5");
        //		}
        return (Int32)EErrorCode.eNormal;
    }

    //已换DisappearInfo
    //已换OnNetMsg_NotifyGameObjectDisAppear
    Int32 OnNetMsg_NotifyGameObjectDisAppear(Stream stream)
    {
        GSToGC.DisappearInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (ulong guid in pMsg.guid)
        {
            UInt64 sGUID = guid;
            if (!EntityManager.AllEntitys.ContainsKey(sGUID))
            {
                continue;
            }

            Ientity sEntity = EntityManager.AllEntitys[sGUID];
            EventCenter.Broadcast<UInt64>(EGameEvent.eGameEvent_RemoveMiniMap, sGUID);
            if (EntityManager.AllEntitys.ContainsKey(sGUID) && CTools.IfTypeHero((EObjectType)sEntity.ObjTypeID))
            {
                PlayerManager.Instance.HideEntity(sGUID);
                continue;
            }

            EntityManager.HandleDelectEntity(sGUID);
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换GO                  （未复查）
    //eMsgToGCFromGS_NotifyGameObjectAppear
    Int32 OnNetMsg_NotifyGameObjectAppear(System.IO.Stream stream)
    {
        OnNetMsg_NotifyGameObjectAppearCoroutine(stream);
        return (Int32)EErrorCode.eNormal;
    }

    public void OnNetMsg_NotifyGameObjectAppearCoroutine(System.IO.Stream stream)
    {
        if (GameStateManager.Instance.GetCurState().GetStateType() == GameStateType.GS_Play)
        {
            GSToGC.GOAppear pMsg;
            if (!ProtoDes(out pMsg, stream))
            {
                return;
            }

            foreach (GSToGC.GOAppear.AppearInfo info in pMsg.info)
            {
                UInt64 sMasterGUID = info.masterguid;
                UInt64 sObjGUID = info.objguid;
                if (sObjGUID < 1)
                {
                    Debug.LogError("objguid:" + sObjGUID);
                }
                Int32 IntCamp = info.camp;
                Vector3 mvPos = this.ConvertPosToVector3(info.pos);
                Vector3 mvDir = this.ConvertDirToVector3(info.dir);
                mvDir.y = 0.0f;
                EntityCampType Type = GameMethod.GetEntityCamp(IntCamp);
                GSToGC.ObjType objType = info.obj_type;

                if (EntityManager.AllEntitys.ContainsKey(sObjGUID))
                {
                    if (objType == GSToGC.ObjType.ObjType_Hero)
                    {
                        PlayerManager.Instance.ShowEntity(sObjGUID, mvPos, mvDir);

                        EventCenter.Broadcast<Ientity>(EGameEvent.eGameEvent_AddMiniMap, EntityManager.AllEntitys[sObjGUID]);
                    }
                    continue;
                }

                Ientity entity = null;
                if (CTools.IfTypeNPC((EObjectType)info.obj_type_id))
                {
                    entity = NpcManager.Instance.HandleCreateEntity(sObjGUID, Type);
                    entity.EntityCamp = Type;
                    entity.ObjTypeID = info.obj_type_id;
                    NpcManager.Instance.CreateEntityModel(entity, sObjGUID, mvDir, mvPos);
                }
                else if (CTools.IfTypeHero((EObjectType)info.obj_type_id))
                {
                    Iplayer player = null;
                    if (!PlayerManager.Instance.AccountDic.TryGetValue(sMasterGUID, out player))
                    {
                        player = (Iplayer)PlayerManager.Instance.HandleCreateEntity(sMasterGUID, Type);
                        player.ObjTypeID = info.obj_type_id;
                        PlayerManager.Instance.AddAccount(sMasterGUID, player);
                    }
                    player.EntityCamp = Type;
                    entity = player;
                    entity.ObjTypeID = info.obj_type_id;
                    PlayerManager.Instance.CreateEntityModel(entity, sObjGUID, mvDir, mvPos);
                    GameMethod.CreateCharacterController(player);
                    ReadPreLoadConfig.Instance.AddPreLoadRoleEffect((int)entity.ObjTypeID);
                }
                if (entity != null)
                {
                    entity.GameObjGUID = sObjGUID;
                    EntityManager.Instance.AddEntity(sObjGUID, entity);
                    if (GameUserModel.Instance.IsLocalPlayer(sMasterGUID))
                    {
                        Debug.Log("Set local player guid!" + sMasterGUID);
                        PlayerManager.Instance.LocalPlayer = (Iselfplayer)entity;
                        GameMethod.GetMainCamera.target = entity.realObject.transform;
                        GamePlayCtrl.Instance.UpdateSkillPriv((int)info.obj_type_id);

                        CPChanggeManager.Instance.ClearList();
                        EventCenter.Broadcast(EGameEvent.eGameEvent_InitMiniMap);
                    }
                    entity.InitWhenCreateModel();
                    entity.OnCreateShadow();
                    entity.GOSSI.sServerBeginPos = mvPos;
                    entity.GOSSI.sServerSyncPos = mvPos;
                    entity.EntityFSMChangedata(mvPos, mvDir);
                    entity.OnFSMStateChange(EntityFreeFSM.Instance);
                    EventCenter.Broadcast<Ientity>(EGameEvent.eGameEvent_AddMiniMap, entity);
                }
            }
        }

        // StartBattle();
    }

    Int32 OnNetMsg_NotifyGameObjectFreeState(Stream stream)
    {
        GSToGC.FreeState pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        if (null == pMsg.dir || null == pMsg.pos)
            return 0;
        UInt64 sGUID;
        sGUID = pMsg.objguid;
        Vector3 mvPos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 mvDir = this.ConvertDirToVector3(pMsg.dir);
        Ientity entity = null;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {

            Vector3 sLastSyncPos = entity.GOSSI.sServerSyncPos;
            mvPos.y = entity.RealEntity.transform.position.y;
            entity.GOSSI.sServerBeginPos = mvPos;
            entity.GOSSI.sServerSyncPos = mvPos;
            entity.GOSSI.sServerDir = mvDir;
            entity.GOSSI.fBeginTime = Time.realtimeSinceStartup;
            entity.GOSSI.fLastSyncSecond = Time.realtimeSinceStartup;
            entity.EntityFSMChangedata(mvPos, mvDir);
//             if (entity is Iselfplayer && VirtualStickUI.Instance.VirtualStickState != VirtualStickUI.StickState.InActiveState)
//             {
//                 entity.OnFSMStateChange(PlayerAdMoveFSM.Instance);
//             }
//             else
            {
                entity.OnFSMStateChange(EntityFreeFSM.Instance);
            }
            //一下内容本身就是被注释掉的
            //if (entity.entityType == EntityType.Player || entity.entityType == EntityType.Soldier || entity.entityType == EntityType.AltarSoldier)
            //{
            //    if (UIMiniMap.Instance != null && PlayerManager.Instance.LocalAccount.ObType == ObPlayerOrPlayer.PlayerObType)
            //    {
            //        UIMiniMap.Instance.AddMapElement(sGUID, entity.EntityCamp, mvPos.x, mvPos.y, mvPos.z);
            //    }
            //}
            //以上内容本身就是被注释掉的
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换RunningState
    //eMsgToGCFromGS_NotifyGameObjectRunState
    Int32 OnNetMsg_NotifyGameObjectRunState(Stream stream)
    {
        GSToGC.RunningState pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        if (null == pMsg.dir || null == pMsg.pos)
            return 0;
        UInt64 sGUID;
        sGUID = pMsg.objguid;
        Vector3 mvPos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 mvDir = this.ConvertDirToVector3(pMsg.dir);
        float mvSp = pMsg.movespeed / 100.0f;
        Ientity entity = null;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            mvPos.y = entity.RealEntity.transform.position.y;
            entity.GOSSI.sServerBeginPos = mvPos;
            entity.GOSSI.sServerSyncPos = mvPos;
            entity.GOSSI.sServerDir = mvDir;
            entity.GOSSI.fServerSpeed = mvSp;
            entity.GOSSI.fBeginTime = Time.realtimeSinceStartup;
            entity.GOSSI.fLastSyncSecond = Time.realtimeSinceStartup;
            entity.EntityFSMChangedata(mvPos, mvDir, mvSp);
            entity.OnFSMStateChange(EntityRunFSM.Instance);
        }
        return (Int32)EErrorCode.eNormal;
    }

    #endregion
    Int32 OnNetMsg_NotifyBattleStateChange(System.IO.Stream stream)
    {
        GSToGC.BattleStateChange pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        if (GameUserModel.Instance.IsReconnect)
        {
            GameUserModel.Instance.IsReconnect = false;

            GameStateType curState = GameStateManager.Instance.GetCurState().GetStateType();
            switch ((e_BattleState)pMsg.state)
            {
                case e_BattleState.eBS_SelectHero:

                    HeroCtrl.Instance.SetSelectState(HeroCtrl.HeroSelectState.EnterSelect);

                    EventCenter.SendEvent(new CEvent(EGameEvent.eGameEvent_IntoHero));

                    EventCenter.Broadcast<int>(EGameEvent.eGameEvent_HeroFirstTime, pMsg.statetimeleft);

                    break;
                case e_BattleState.eBS_SelectRune:

                    HeroCtrl.Instance.SetSelectState(HeroCtrl.HeroSelectState.EnterSelect);

                    EventCenter.SendEvent(new CEvent(EGameEvent.eGameEvent_IntoHero));

                    EventCenter.Broadcast<int>(EGameEvent.eGameEvent_HeroSecondTime, pMsg.statetimeleft);

                    HeroCtrl.Instance.SetSelectState(HeroCtrl.HeroSelectState.RandomSelect);

                    break;
                case e_BattleState.eBS_Loading:
                    if (curState < GameStateType.GS_Loading)
                    {
                        CEvent evt = new CEvent(EGameEvent.eGameEvent_Loading);
                        evt.AddParam("NextState", BlGame.GameState.GameStateType.GS_Play);
                        EventCenter.SendEvent(evt);
                    }
                    else if (curState > GameStateType.GS_Loading)
                    {
                        CGLCtrl_GameLogic.Instance.AskLoadComplete();
                    }

                    break;
                case e_BattleState.eBS_Playing:
                    if (curState < GameStateType.GS_Loading)
                    {
                        CEvent evt2 = new CEvent(EGameEvent.eGameEvent_Loading);
                        evt2.AddParam("NextState", BlGame.GameState.GameStateType.GS_Play);
                        EventCenter.SendEvent(evt2);
                        Debug.LogWarning("battle play ");
                    }
                    else
                    {
                        CGLCtrl_GameLogic.Instance.AskLoadComplete();
                        Debug.LogWarning("battle play but send loadcomplete!");
                    }
                    break;
                case e_BattleState.eBS_Finished:
                    {
                        EventCenter.Broadcast(EGameEvent.eGameEvent_BatttleFinished);
                    }
                    break;
            }
        }
        else
        {
            GameStateType curState = GameStateManager.Instance.GetCurState().GetStateType();

            switch ((e_BattleState)pMsg.state)
            {

                case e_BattleState.eBS_SelectHero:
                    {
                        if (JxBlGame.Instance.IsQuickBattle)
                        {
                            EventCenter.SendEvent(new BlGame.CEvent(EGameEvent.eGameEvent_IntoHero));
                            JxBlGame.Instance.IsQuickBattle = false;
                        }
                        else
                        {
                            EventCenter.Broadcast(EGameEvent.eGameEvent_RoomEnd);
                        }

                        EventCenter.Broadcast<int>(EGameEvent.eGameEvent_HeroFirstTime, pMsg.statetimeleft);
                    }
                    break;
                case e_BattleState.eBS_SelectRune:
                    {
                        EventCenter.Broadcast<int>(EGameEvent.eGameEvent_HeroSecondTime, pMsg.statetimeleft);

                        HeroCtrl.Instance.SetSelectState(HeroCtrl.HeroSelectState.RandomSelect);
                    }
                    break;
                case e_BattleState.eBS_Loading:
                    {
                        CEvent evt = new CEvent(EGameEvent.eGameEvent_Loading);
                        evt.AddParam("NextState", BlGame.GameState.GameStateType.GS_Play);
                        EventCenter.SendEvent(evt);
                    }
                    break;
                case e_BattleState.eBS_Playing:
                    {
                        if (curState < GameStateType.GS_Play)
                        {
                            CEvent evt = new CEvent(EGameEvent.eGameEvent_Loading);
                            evt.AddParam("NextState", BlGame.GameState.GameStateType.GS_Play);
                            EventCenter.SendEvent(evt);
                        }
                        else
                        {
                            //进入游戏
                            if (LoadScene.Instance != null)
                            {
                                //进入游戏前需要强制清除界面贴图资源
                                LoadScene.Instance.ReleaseResource();
                                LoadScene.Instance.CloseLoading();
                            }
                        }
                    }
                    break;
                case e_BattleState.eBS_Finished:
                    {
                        if (curState < GameStateType.GS_Play)
                        {
                            if (LoadScene.Instance != null)
                            {
                                LoadScene.Instance.CloseLoading();
                            }
                            CGLCtrl_GameLogic.Instance.EmsgToss_AskReEnterRoom();
                        }
                    }
                    break;
            }
        }

        return (Int32)EErrorCode.eNormal;
    }

    //已换CurBattleChange（未复查） 
    //eMsgToGCFromGS_NotifyCurBattleChange
    Int32 OnNetMsg_NotifyCurBattleChange(System.IO.Stream stream)
    {
        GSToGC.CurBattleChange pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        if (pMsg.battleid != 0)
        {
            GameUserModel.Instance.GameBattleID = pMsg.battleid;
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyBattleBaseInfo(System.IO.Stream stream)
    {
        GSToGC.BattleBaseInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        GameUserModel.Instance.GameBattleID = pMsg.battleid;
        GameUserModel.Instance.GameMapID = pMsg.mapid;
        GameUserModel.Instance.IsReconnect = pMsg.ifReconnect;

        if (pMsg.ifReconnect)
        {
            GameUserModel.Instance.GameBattleID = pMsg.battleid;
            GameUserModel.Instance.GameMapID = (UInt32)pMsg.mapid;
            EventCenter.Broadcast(EGameEvent.eGameEvent_ReconnectToBatttle);
        }
        else
        {
            CGLCtrl_GameLogic.Instance.EmsgToss_AskEnterBattle(pMsg.battleid);
        }
        return (Int32)EErrorCode.eNormal;
    }


    Int32 OnNetMsg_NotifyRoomBaseInfo(System.IO.Stream stream)
    {
        GSToGC.RoomBaseInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        RoomCtrl.Instance.UpdateRoomBaseInfo(pMsg.roomid, pMsg.mapid);

        return (Int32)EErrorCode.eNormal;
    }

    //已换BattleSeatPosInfo（未复查）
    //eMsgToGCFromGS_NotifyBattleSeatPosInfo
    Int32 OnNetMsg_NotifyBattleSeatPosInfo(System.IO.Stream stream)
    {
        GSToGC.BattleSeatPosInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (GSToGC.BattleSeatPosInfo.PosInfo posinfo in pMsg.posinfo)
        {
            UInt64 sGUID = posinfo.guid;
            Iplayer entity = null;
            UInt64 id = sGUID;
            if (id == 0)
            {
                PlayerManager.Instance.RemoveAccountBySeat((uint)posinfo.pos);
            }
            else
            {
                if (!PlayerManager.Instance.AccountDic.TryGetValue(sGUID, out entity))
                {
                    entity = (Iplayer)PlayerManager.Instance.HandleCreateEntity(sGUID, EntityCampType.CampTypeNull);
                    PlayerManager.Instance.AddAccount(sGUID, entity);
                    if (GameUserModel.Instance.IsLocalPlayer(sGUID))
                    {
                        PlayerManager.Instance.LocalAccount = entity;
                    }

                }
            }
            if (entity != null)
            {
                entity.SetSeatPosInfo((uint)posinfo.pos, posinfo.ifmaster, posinfo.ifready, posinfo.nickname, posinfo.headid, (int)posinfo.gold);
            }
        }
        EventCenter.Broadcast(EGameEvent.eGameEvent_SeatPosUpdate);
        return (Int32)EErrorCode.eNormal;
    }

    //已换NetClash（未复查）
    //eMsgToGCFromGS_NotifyNetClash
    Int32 OnNetMsg_NotifyNetClash(Stream stream)
    {
        EventCenter.Broadcast<EMessageType>(EGameEvent.eGameEvent_ShowMessage, EMessageType.EMT_KickOut);

        NetworkManager.Instance.canReconnect = false;
        NetworkManager.Instance.Close();

        return (Int32)EErrorCode.eNormal;
    }

    //已换PingRet
    //eMsgToGCFromGS_GCAskPingRet
    Int32 OnNetMsg_NotifyPing(Stream stream)
    {
        GSToGC.PingRet pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        Int64 n64NowTime = CGLCtrl_GameLogic.Instance.GetNowTime();
        float ping = CGLCtrl_GameLogic.Instance.GetDuration(n64NowTime, pMsg.time);
        if (pMsg.flag == 0)
        {
            ShowFPS.Instance.cSPing = ping;
        }
        else
        {
            ShowFPS.Instance.sSPing = ping;
            CEvent eve = new CEvent(EGameEvent.eGameEvent_SSPingInfo);
            eve.AddParam("ping", ping);
            EventCenter.SendEvent(eve);
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyReturn(Stream stream)
    {
        GSToGC.AskRet pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        var askID = pMsg.askid;
        switch (askID)
        {
            case (int)GCToCS.MsgNum.eMsgToGSToCSFromGC_AskBuyGoods:
                Debug.LogError("购买失败!");
                break;
            case (int)GCToCS.MsgNum.eMsgToGSToCSFromGC_EuipRunes:
                Debug.LogError("装备符文失败！");
                break;
            case (int)GCToCS.MsgNum.eMsgToGSToCSFromGC_UnloadRunes:
                Debug.LogError("卸载符文失败！");
                break;
            case (int)GCToCS.MsgNum.eMsgToGSToCSFromGC_ComposeRunes:
                Debug.LogError("合成符文失败！");
                break;
            case (int)GCToCS.MsgNum.eMsgToGSToCSFromGC_AskRecoinRune:
                Debug.LogError("洗练符文失败！");
                break;
        }

        Int32 m_n32ErrorId = pMsg.errorcode;

        switch ((EErrorCode)m_n32ErrorId)
        {
            //case EErrorCode.eEC_NickNameCollision:
            //case EErrorCode.eEc_NickNameContainInvalideChar:
            //    EventCenter.Broadcast(EGameEvent.eGameEvent_AskComplementedRegisterInfo, (EErrorCode)m_n32ErrorId);
            //    break;
            case EErrorCode.eEC_TheBattleUserFull:

            case EErrorCode.eEC_BattlePDWNotMatch:
            case EErrorCode.eEC_InvalidMapID:
            case EErrorCode.eEC_JustInBattle:
            case EErrorCode.eEC_AddBattleFailForLackOfGold:
            case EErrorCode.eEC_BattleIsPlaying:
            case EErrorCode.eEC_UserInBlackList:
            case EErrorCode.eEC_AddBattleFailForUserFull:
            case EErrorCode.eEC_AddBattleFailForAllFull:
            case EErrorCode.eEC_CounterpartFriendListFull:
            case EErrorCode.eEC_BlackListFull:
                EventCenter.Broadcast<EErrorCode>(EGameEvent.eGameEvent_AskFriendEorr, (EErrorCode)m_n32ErrorId);
                break;
            case EErrorCode.eEC_JustNotInBattle:
            case EErrorCode.eEC_YouAreNotBattleManager:
            case EErrorCode.eEC_NotAllUserReady:
            case EErrorCode.eEC_CampNotBalance:
            case EErrorCode.eEC_InvalidBattlePos:
                EventCenter.Broadcast(EGameEvent.eGameEvent_AskBeginBattleError, (EErrorCode)m_n32ErrorId);
                break;
            case EErrorCode.eEC_NullGateServer:
            case EErrorCode.eEC_InvalidUserName:
            case EErrorCode.eEC_InvalidUserPwd:
            case EErrorCode.eEC_UserInfoUnComplete:
                LoginCtrl.Instance.LoginError(m_n32ErrorId);
                break;
            case EErrorCode.eEC_NullUser:
                LoginCtrl.Instance.LoginError(m_n32ErrorId);
                EventCenter.Broadcast(EGameEvent.eGameEvent_AskAddInBattle, (EErrorCode)m_n32ErrorId);
                EventCenter.Broadcast(EGameEvent.eGameEvent_NotEnoughGold, (EErrorCode)m_n32ErrorId);
                break;

            case EErrorCode.eEC_HeroNotDead:
            case EErrorCode.eEC_NoRebornTimes:
            case EErrorCode.eEC_NotEnoughGold:
                EventCenter.Broadcast(EGameEvent.eGameEvent_NotEnoughGold, (EErrorCode)m_n32ErrorId);
                break;
            case EErrorCode.eEC_NullBattle:
                EventCenter.Broadcast(EGameEvent.eGameEvent_AskAddInBattle, (EErrorCode)m_n32ErrorId);
                EventCenter.Broadcast(EGameEvent.eGameEvent_NotEnoughGold, (EErrorCode)m_n32ErrorId);
                EventCenter.Broadcast(EGameEvent.eGameEvent_BattleUpdateRoomList);
                break;
            case EErrorCode.eNormal:
                break;
            case EErrorCode.eEC_BattleClosing:
                EventCenter.Broadcast(EGameEvent.eGameEvent_ReConnectFail);
                EventCenter.Broadcast(EGameEvent.eGameEvent_BeginWaiting);
                MsgInfoManager.Instance.ShowMsg(m_n32ErrorId);
                break;
            case EErrorCode.eEC_InvalidBattleID:
                PlayerManager.Instance.CleanAccount();

                CEvent evt = new CEvent(EGameEvent.eGameEvent_Loading);
                evt.AddParam("NextState", BlGame.GameState.GameStateType.GS_Lobby);
                EventCenter.SendEvent(evt);
                break;
            case EErrorCode.eEC_ReEnterRoomFail:
                EventCenter.SendEvent(new CEvent(EGameEvent.eGameEvent_IntoLobby));
                break;
            case EErrorCode.eEC_AccountTheGame:
                LoginCtrl.Instance.SdkLogOff();
                break;
            
            default:
                MsgInfoManager.Instance.ShowMsg(m_n32ErrorId);
                break;
        }

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyUserBaseInfo(System.IO.Stream stream)
    {
        GSToGC.UserBaseInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        UInt64 sGUID = pMsg.guid;
        if (pMsg.nickname.Count() > 1)
        {
            GameUserModel.Instance.SetGameBaseInfo(pMsg);
            EventCenter.SendEvent(new CEvent(EGameEvent.eGameEvent_IntoLobby));
        }
        else if (sGUID > 0)
        {
            //没有昵称，进入补充玩家信息界面
            EventCenter.SendEvent(new CEvent(EGameEvent.eGameEvent_InputUserData));
        }

        return (Int32)EErrorCode.eNormal;
    }

    //已换HeroList
    //eMsgToGCFromGS_NotifyHeroList
    Int32 OnNetMsg_NotifyHeroList(Stream stream)
    {
        GSToGC.HeroList pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (int heroId in pMsg.heroid)
        {
            GameUserModel.Instance.CanChooseHeroList.Add(heroId);
        }

        GameUserModel.Instance.STCTimeDiff = pMsg.timeDiff;  //ss 和 client 时差
        
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyCSHeroList(Stream stream)
    {
        GSToGC.NotifyCSHeroList pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (GSToGC.NotifyCSHeroList.HeroListCfg hero in pMsg.herocfg)
        {
            CommodityInfos info = new CommodityInfos();
            info.Expired_time = hero.expired_time;
            info.goodsId = (int)hero.heroid;
            info.If_free = hero.if_free;
            info.GoodsType = MarketGoodsType.GoodsTypeHero;
            if (ConfigReader.HeroBuyXmlInfoDict.ContainsKey(info.goodsId))
            {
                GameUserCtrl.Instance.DeltGetHeroInfoData(info);
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyRoomChat(Stream stream)
    {
        GSToGC.ChatInRoom pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        Int32 m_un8Seat = pMsg.pos;
        string str = pMsg.chat;

        RoomCtrl.Instance.RecvTalkMessage((uint)m_un8Seat, str);

        return (Int32)EErrorCode.eNormal;
    }

    //已换NotifyChooseHeroTimeEnd
    //eMsgToGCFromGS_NotifyChooseHeroTimeEnd
    Int32 OnNetMsg_NotifyToChooseHero(Stream stream)
    {
        //uint m_un8Seat = pcMsg.GetByte();
        //UInt32 heroId = pcMsg.GetUInt32();
        //SelectHeroData.Instance().AddPreSelectHero(m_un8Seat, (int)heroId);
        //return (Int32)EErrorCode.eNormal;

        GSToGC.TryToChooseHero pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        uint m_un8Seat = (uint)pMsg.pos;
        int heroId = pMsg.heroid;
        HeroCtrl.Instance.AddPreSelectHero(m_un8Seat, heroId);
        return (Int32)EErrorCode.eNormal;
    }

    //已换HeroInfo
    //eMsgToGCFromGS_NotifyBattleHeroInfo
    Int32 OnNetMsg_NotifyBattleHeroInfo(Stream stream)
    {
        GSToGC.HeroInfo pMsg = ProtoBuf.Serializer.Deserialize<GSToGC.HeroInfo>(stream);
        if (null == pMsg)
        {
            Debug.LogError("TODO Replace"); return PROTO_DESERIALIZE_ERROR;
        }
        HeroCtrl.Instance.AddRealSelectHero((uint)pMsg.heroposinfo.pos, pMsg.heroposinfo.heroid);   //ToReview int->uint
        return (Int32)EErrorCode.eNormal;
    }

    //已换FPInfo
    //eMsgToGCFromGS_NotifyFightPropertyInfo
    Int32 OnNetMsg_NotifyFightPropertyInfo(Stream stream)
    {
        GSToGC.NotifyAFPData pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        Ientity entity;
        UInt64 sGUID;
        sGUID = pMsg.guid;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            foreach (GSToGC.NotifyAFPData.FPInfo fpInfo in pMsg.info)
            {
                switch ((GSToGC.NotifyAFPData.EnumFpType)fpInfo.type)
                {
                    case GSToGC.NotifyAFPData.EnumFpType.PhyAttack:
                        {
                            entity.PhyAtk = fpInfo.value;
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.MagicAttack:
                        {
                            entity.MagAtk = fpInfo.value;
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.PhyDefense:
                        {
                            entity.PhyDef = fpInfo.value;
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.MagicDefense:
                        {
                            entity.MagDef = fpInfo.value;
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.MoveSpeed:
                        {
                            entity.OnEntityMoveSpeedChange(fpInfo.value);
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.AttackSpeed:
                        {
                            entity.AtkSpeed = fpInfo.value;

                            if (entity.AtkSpeed != 0)
                            {
                                float spd = 1 / (entity.AtkSpeed / 1000.0f);
                                entity.RealEntity.SetAttackAnimationSpd(spd);
                            }
                            else
                            {
                                entity.RealEntity.SetAttackAnimationSpd(1.0f);
                            }
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.AttackDist:
                        {
                            entity.AtkDis = fpInfo.value / 100;
                            EventCenter.Broadcast(EGameEvent.eGameEvent_LocalPlayerRange);
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.MaxHP:
                        {
                            entity.SetHpMax(fpInfo.value);

                            if (entity.XueTiao != null)
                            {
                                entity.XueTiao.SetXueTiaoInfo();
                            }
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.MaxMP:
                        {
                            entity.SetMpMax(fpInfo.value);
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.HPRecoverRate:
                        {
                            entity.HpRecover = fpInfo.value;
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.MPRecoverRate:
                        {
                            entity.MpRecover = fpInfo.value;
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.ReliveTime:
                        {
                            entity.RebornTime = fpInfo.value;
                        }
                        break;
                    case GSToGC.NotifyAFPData.EnumFpType.CooldownReduce:
                        {
                            entity.CooldownReduce = fpInfo.value;
                        }
                        break;
                    }
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换NotifyHPInfo
    //eMsgToGCFromGS_NotifyHPInfo
    Int32 OnNetMsg_NotifyHpInfo(Stream stream)
    {
        GSToGC.NotifyHPInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (GSToGC.NotifyHPInfo.HPInfo info in pMsg.hpinfo)
        {
            UInt64 sGUID;
            sGUID = info.guid;
            Ientity entity;
            if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
            {
                entity.SetHp((float)info.curhp);
                entity.SetHpMax((float)info.maxhp);


                entity.OnUpdateHp();
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换NotifyMPInfo
    //eMsgToGCFromGS_NotifyMPInfo
    Int32 OnNetMsg_NotifyMpInfo(Stream stream)
    {
        GSToGC.NotifyMPInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        foreach (GSToGC.NotifyMPInfo.MPInfo info in pMsg.mpinfo)
        {
            UInt64 sGUID;
            sGUID = info.guid;
            Ientity entity;
            if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
            {
                entity.SetMp((float)info.curmp);        //ToReview int->float（是否本身就是float，有没有丢失小数部分）
                entity.SetMpMax((float)info.maxmp);     //ToReview int->float
                if (entity is Iplayer)
                {
                    XueTiaoPlayer playerXueTiao = (XueTiaoPlayer)entity.XueTiao;
                    playerXueTiao.UpdateMp();
                }
            }
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyHeroInfo(Stream stream)
    {
        GSToGC.NotifyHeroInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sGUID;
        sGUID = pMsg.guid;     //Review long->ulong
        Ientity entity = null;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            entity.SetLevel(pMsg.level);
            entity.SetExp((float)pMsg.exp);
            Iplayer player = (Iplayer)entity;
            player.SetFuryState((EFuryState)pMsg.fury);
            XueTiaoPlayer xueTiaoPlayer = (XueTiaoPlayer)entity.XueTiao;
            xueTiaoPlayer.UpdateLevel();
            xueTiaoPlayer.SetXueTiaoInfo();
            EventCenter.Broadcast(EGameEvent.eGameEvent_HeroInfoChange, (Iplayer)entity);
            //ToReview absorb1和absorb2没用上
        }
        return (Int32)EErrorCode.eNormal;
    }

    //战斗结束
    //已换BattleFinishInfo
    //eMsgToGCFromGS_NotifyBattleFinish
    Int32 OnNetMsg_NotifyBattleFinish(Stream stream)
    {
        GSToGC.BattleFinish pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        EventCenter.Broadcast<UInt64>(EGameEvent.eGameEvent_GameOver, pMsg.bulidguid);
        return (Int32)EErrorCode.eNormal;
    }

    //主角经验
    //已换Exp
    //eMsgToGCFromGS_NotifyExpInfo
    Int32 OnNetMsg_NotifyExpInfo(Stream stream)
    {
        if (PlayerManager.Instance.LocalPlayer == null)
        {
            return (Int32)EErrorCode.eNormal;
        }
        GSToGC.Exp pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sGUID;
        sGUID = pMsg.guid;
        PlayerManager.Instance.LocalPlayer.SetExp((float)pMsg.exp);
        return (Int32)EErrorCode.eNormal;
    }

    //主角等级
    /************************************************************************/
    /*OnNetMsg_NotifyLvInfo                                                 */
    /************************************************************************/
    Int32 OnNetMsg_NotifyLvInfo(Stream stream)
    {
        StartCoroutine(LvInfoCoroutine(stream));
        return (Int32)EErrorCode.eNormal;
    }


    public IEnumerator LvInfoCoroutine(Stream stream)
    {
        //解析消息
        GSToGC.LevelInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            yield break;
        }

        //创建特效
        yield return 1;
        UInt64 sGUID;
        sGUID = pMsg.guid;
        Int32 level = pMsg.level;
        Ientity entity;
        if (EntityManager.AllEntitys.TryGetValue(sGUID, out entity))
        {
            if (entity.Level != level)
            {
                BlGame.Effect.EffectManager.Instance.CreateTimeBasedEffect("effect/other/shengji", 10.0f, entity);

                yield return 1;
                Vector3 pos = PlayerManager.Instance.LocalPlayer.RealEntity.transform.position;
                if (entity.GetDistanceToPos(pos) <= 30)
                {
                    ResourceUnit unit = ResourcesManager.Instance.loadImmediate(AudioDefine.PATH_LEVELUP_SOUND, ResourceType.ASSET);
                    AudioClip clip = unit.Asset as AudioClip;

                    AudioManager.Instance.PlayEffectAudio(clip);
                }
            }

            //更新level
            yield return 1;
            entity.SetLevel(level);
            if (entity.XueTiao is XueTiaoPlayer)
            {
                XueTiaoPlayer xueTiaoPlayer = (XueTiaoPlayer)entity.XueTiao;
                xueTiaoPlayer.UpdateLevel();
            }
        }
        if (UIGameBattleInfo.Instance != null)
        {
            UIGameBattleInfo.Instance.SetLevelInfo(sGUID, (int)level);
        }
        if (UIViewerBattleInfo.Instance != null)
        {
            UIViewerBattleInfo.Instance.SetLevelInfo(sGUID, (int)level);
        }
        BattleingData.Instance.AddPlayer(sGUID, level, BattleDataType.Level);
        EventCenter.Broadcast(EGameEvent.eGameEvent_AllPlayerLevel);
        EventCenter.Broadcast(EGameEvent.eGameEvent_HeroLvChange, (Iplayer)entity);

    }


    Int32 OnNetMsg_NotifyFuryValue(Stream stream)
    {
        GSToGC.FuryVal pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sGUID;
        sGUID = pMsg.guid;
        if (PlayerManager.Instance.LocalPlayer != null)
        {
            PlayerManager.Instance.LocalPlayer.SetFuryValue(pMsg.fury);
        }
        return (Int32)EErrorCode.eNormal;
    }

    //已换FuryState
    //eMsgToGCFromGS_NotifyFuryState
    Int32 OnNetMsg_NotifyFuryState(Stream stream)
    {
        GSToGC.FuryState pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 sGUID;
        sGUID = (ulong)pMsg.guid;
        Ientity entity = PlayerManager.Instance.GetEntity(sGUID);
        if (entity != null)
        {
            Iplayer player = (Iplayer)entity;
            player.SetFuryState((EFuryState)pMsg.state);
            PlayerSkillData.Instance.SetPlayerFuryState(player, (EFuryState)pMsg.state);
            EventCenter.Broadcast(EGameEvent.eGameEvent_SkillCDInfo, (Iplayer)entity);
        }
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyVoipRoomId(CMsg pcMsg)
    {
        long roomId = pcMsg.GetInt64();
        return (Int32)EErrorCode.eNormal;
    }

    //已换（见OnNotifyBornSoldierCoroutine函数）
    Int32 OnNotifyBornSoldier(Stream stream)
    {
        OnNotifyBornSoldierCoroutine(stream);
        return (Int32)EErrorCode.eNormal;
    }

    public Int32 OnNotifyBornSoldierCoroutine(Stream stream)
    {
        GSToGC.BornSoler pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return 0;
        }
        int type = pMsg.type;// 1 altar solder ; 2 atk city solder 3 bomb soldier
        int campType = pMsg.camp;
        string[] effectPath = {
                                    "effect/building/Altar",
                                    "effect/building/Altar_red",
                                };
        int redIndex = (campType % 2 == 0) ? 1 : 0;

        NormalEffect effect = EffectManager.Instance.CreateNormalEffect(effectPath[redIndex], null);
        effect.obj.transform.position = ConvertPosToVector3(pMsg.pos);

        UInt64 guid;
        guid = pMsg.guid;
        MsgInfoManager.Instance.SetAudioPlay(guid, MsgInfoManager.AudioPlayType.KillAudio);

        return 0;
    }

    //已换
    void OnNetMsg_WarningToSelectHero()
    {
        if (GameStateManager.Instance.GetCurState().GetStateType() == GameStateType.GS_Hero)
        {
            MsgInfoManager.Instance.ShowMsg((int)ERROR_TYPE.eT_WarnningToSelectHero);
        }
    }

    Int32 OnNetMsg_NotifyNewsGuideReConnect(Stream stream)
    {
        GSToGC.GuideLastStep pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        int lastId = pMsg.stepid;
        SceneGuideTaskManager.Instance().InitSenseTaskData();
        SceneGuideTaskManager.Instance().SetHasFinishedAllGuide(pMsg.ifComp);
        if (pMsg.ifComp || lastId == 0)//finish all or first 
        {
            SceneGuideTaskManager.Instance().StartAsignedStep(1001);
            return (Int32)EErrorCode.eNormal;
        }
        else if (lastId != 0)
        {//这个是新手引导接着上面一步            
            SceneGuideTaskManager.Instance().ReConnectTask(lastId);
        }
        return (Int32)EErrorCode.eNormal;
    }

    //ToReviw 未换（消息参数不对应）
    //协议名不一致eMsgToGCFromCS_NotifyReconnectHeroGoodsInfo
    //     Int32 OnNetMsg_NotifyReconnectHeroGoods(CMsg pcMsg)
    //     {
    //         UInt64 targetID = pcMsg.GetGUID();
    //         int time = pcMsg.GetInt32();
    //         UInt32 num = pcMsg.GetUInt32();
    //         //GameMethod.DebugError("time = " + time);
    //         if (UIGameRecord.Instance != null)
    //         {
    //             UIGameRecord.Instance.SetReconnectTime(time);
    //         }
    //         for (int i = 0; i < num; i++)
    //         {
    //             byte itPos = pcMsg.GetByte();
    //             int m_32Type = (int)pcMsg.GetUInt32();
    //             int m_n32Num = pcMsg.GetInt32();
    // 
    //             int n32RemainCDTime = pcMsg.GetInt32();
    //             if (n32RemainCDTime < 0)
    //             {
    //                 n32RemainCDTime = 0;
    //             }
    //             float totTime = 0.0f;
    //             if (true == ConfigReader.ItemXmlInfoDict.ContainsKey(m_32Type))
    //             {
    //                 totTime = ConfigReader.ItemXmlInfoDict[m_32Type].un32CdTime;
    //             }
    // 
    //             float second = (float)n32RemainCDTime / 1000.0f;
    // 
    //             if (PlayerManager.Instance.LocalAccount.ObType == ObPlayerOrPlayer.PlayerType)
    //             {
    //                 PlayerManager.Instance.LocalPlayer.AddUserGameItems((int)itPos, m_32Type, m_n32Num, second);
    //             }
    //             Ientity entity;
    //             if (EntityManager.AllEntitys.TryGetValue(targetID, out entity))
    //             {
    //                 PackageData.Instance.SetPackageData((Iplayer)entity, (int)itPos, m_32Type, m_n32Num, totTime, second);
    //                 EventCenter.Broadcast(EGameEvent.eGameEvent_PackageBuyInfo, (Iplayer)entity);
    //             }
    //             EventCenter.Broadcast(EGameEvent.eGameEvent_UpdateUserGameItems);
    //         }
    //         EventCenter.Broadcast<Int64>(EGameEvent.eGameEvent_GameStartTime, time);
    //         return (Int32)EErrorCode.eNormal;
    //     }

    //已换
    Int32 OnNetMsg_NotifyReconnectInfo(Stream stream)
    {
        GSToGC.NotifyReconnectInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        EventCenter.Broadcast(EGameEvent.eGameEvent_ReConnectSuccess);
        e_BattleState battleState = (e_BattleState)pMsg.battlestate;
        Dictionary<uint, int> heroSelectDic = new Dictionary<uint, int>();
        Dictionary<uint, bool> heroIsSelectDic = new Dictionary<uint, bool>();
        foreach (GSToGC.NotifyReconnectInfo.ReconnectInfo info in pMsg.reconnectinfo)
        {
            int seat = info.pos;
            UInt64 guid;
            guid = info.guid;
            int heroId = (int)info.heroid;      //ToReview uint->int
            Iplayer player = null;
            if (!PlayerManager.Instance.AccountDic.TryGetValue(guid, out player))
            {
                player = (Iplayer)PlayerManager.Instance.HandleCreateEntity(guid, EntityCampType.CampTypeNull);
            }
            player.SetReconnectPlayerInfo((uint)seat, info.nickname);
            if (!PlayerManager.Instance.AccountDic.ContainsKey(guid))
            {
                PlayerManager.Instance.AddAccount(guid, player);
            }
            ObPlayerOrPlayer obType = (seat == 7 || seat == 8) ? ObPlayerOrPlayer.PlayerObType : ObPlayerOrPlayer.PlayerType;
            player.SetObjType(obType);
            if (GameUserModel.Instance.IsLocalPlayer(guid))
            {
                PlayerManager.Instance.LocalAccount = player;
            }
            if (heroId != 0 && obType != ObPlayerOrPlayer.PlayerObType)
            {
                heroSelectDic.Add((uint)seat, heroId);
                heroIsSelectDic.Add((uint)seat, info.ifselected);
            }
        }


        //更新英雄选择数据
        if (battleState == e_BattleState.eBS_SelectRune || battleState == e_BattleState.eBS_SelectHero)
        {
            foreach (var item in heroSelectDic.Keys)
            {
                int heroId = 0;
                bool isSelect = false;
                if (!heroSelectDic.TryGetValue(item, out heroId))
                {
                    continue;
                }
                heroIsSelectDic.TryGetValue(item, out isSelect);
                if (isSelect)
                {
                    HeroCtrl.Instance.AddRealSelectHero(item, heroId);
                }
                else
                {
                    HeroCtrl.Instance.AddPreSelectHero(item, heroId);

                    if (item == (int)PlayerManager.Instance.LocalAccount.GameUserSeat)
                    {
                        HeroCtrl.Instance.ReconnectPreSelect(heroId);
                    }
                }
            }
        }

        return (Int32)EErrorCode.eNormal;
    }

    //已换
    Int32 OnNetMsg_NotifyUserGuideSetUps(Stream stream)
    {
        if (JxBlGame.Instance.SkipNewsGuide)
        {
            IGuideTaskManager.Instance().SetTaskIsFinish(true, true);
            return (Int32)EErrorCode.eNormal;
        }

        GSToGC.GuideSteps pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        UInt64 guide;
        guide = pMsg.guid;
        Int32 isLineGuide = pMsg.type;
        bool isGuideOver = pMsg.ifComp; // 1 over, 0 no over
        string str = pMsg.steps;
        IGuideTaskManager.Instance().SetTaskIsFinish((isLineGuide == 1 ? true : false), isGuideOver);

        if (isLineGuide == 1 && !isGuideOver)//line task
        {
            int taskId = (str.Length == 0) ? 0 : Convert.ToInt32(str);
            bool isStart = (str.Length == 0) ? true : false;
            if (isStart)
            {
                //Debug.LogError("isStart");
                taskId = IGuideTaskManager.startId;
            }
            else
            {
                //Debug.LogError(taskId);
                List<int> idList = ConfigReader.GetIGuideManagerInfo(taskId).NextTaskId;
                taskId = idList.ElementAt(0);
            }
            IGuideTaskManager.Instance().SetTaskId(taskId);
        }
        else if (isLineGuide == 2 && !isGuideOver && IGuideTaskManager.Instance().IsLineTaskFinish())//trigger task
        {
            IGuideTaskManager.Instance().SetHasTriggerTask(str);
            IGuideTaskManager.Instance().StartTriggerTask();
        }


        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifySendSoldierTip(Stream stream)
    {
        GSToGC.GameTips pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }
        if (SceneGuideTaskManager.Instance().IsNewsGuide() != SceneGuideTaskManager.SceneGuideType.NoGuide)
            return (Int32)EErrorCode.eNormal;
        int type = pMsg.errocode;
        int camp = pMsg.campid;
        string campTip = null;
        if (type == -130825)
        {//超级兵
            if (camp % 2 != 0)//打爆亡灵族
            {
                campTip = "亡灵势力";
            }
            else//打爆精灵族
            {
                campTip = "精灵势力";
            }
            MsgConfigInfo msg = new MsgConfigInfo(ConfigReader.GetMsgInfo(type));
            msg.content = campTip + msg.content;
            MsgInfoManager.Instance.ShowMsg(msg);
        }
        else
        {
            MsgInfoManager.Instance.ShowMsg(type);
        }

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifySecondaryGuide(Stream stream)
    {
        GSToGC.SecondGuideTask pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        if (SceneGuideTaskManager.Instance().IsNewsGuide() != SceneGuideTaskManager.SceneGuideType.NoGuide)
            return (Int32)EErrorCode.eNormal;
        SecondaryGuideManager.Instance.CleanAll();
        foreach (GSToGC.SecondGuideTask.task_info info in pMsg.taskinfo)
        {
            int taskId = info.taskid;
            int matches = info.num;
            SecondaryGuideManager.Instance.InitFinishTask(taskId, matches);
        }
        SecondaryGuideManager.Instance.InitTask();
        SecondaryGuideManager.Instance.StartAllTask();
        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyMatchTeamBaseInfo(Stream stream)
    {
        GSToGC.NotifyMatchTeamBaseInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        TeamMatchCtrl.Instance.InitTeamBaseInfo(pMsg.mapid, pMsg.matchtype);

        if (pMsg.teamid != 0)
        {
            TeamMatchCtrl.Instance.Enter();
        }
        else
        {
            TeamMatchCtrl.Instance.Exit();
        }

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyMatchTeamPlayerInfo(Stream stream)
    {
        GSToGC.NotifyMatchTeamPlayerInfo pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        if (pMsg.isInsert)
        {
            TeamMatchCtrl.Instance.AddTeammate(pMsg.postion, pMsg.nickname, pMsg.headid.ToString(), pMsg.userlevel);
        }
        else
        {
            TeamMatchCtrl.Instance.DelTeammate(pMsg.nickname);
        }

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyMatchTeamSwitch(Stream stream)
    {
        GSToGC.NotifyMatchTeamSwitch pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        if (pMsg.startflag)
        {
            JxBlGame.Instance.IsQuickBattle = true;
            TeamMatchCtrl.Instance.StartMatchSearching();
        }
        else
        {
            TeamMatchCtrl.Instance.StopMatchSearching();
        }

        return (Int32)EErrorCode.eNormal;
    }

    Int32 OnNetMsg_NotifyMatchInviteJoin(Stream stream)
    {
        if (GameStateManager.Instance.GetCurState().GetStateType() != GameStateType.GS_Lobby)
            return (Int32)EErrorCode.eNormal;

        GSToGC.NotifyMatchInviteJoin pMsg;
        if (!ProtoDes(out pMsg, stream))
        {
            return PROTO_DESERIALIZE_ERROR;
        }

        TeamMatchCtrl.Instance.ShowInvitation(pMsg.nickname);

        return (Int32)EErrorCode.eNormal;
    }

    //工具函数GSToGC.Pos转Vector3（会将厘米转成米）
    private Vector3 ConvertPosToVector3(GSToGC.Pos pos)
    {
        if (pos != null)
            return new Vector3((float)pos.x / 100.0f, GetGlobalHeight(), (float)pos.z / 100.0f);
        else
            return Vector3.zero;
    }

    //工具函数GSToGC.Dir转Vector3
    private Vector3 ConvertDirToVector3(GSToGC.Dir dir)
    {
        float angle = (float)(dir.angle) / 10000;
        return new Vector3((float)Math.Cos(angle), 0, (float)Math.Sin(angle));
    }

    //封装proto解析
    private bool ProtoDes<T>(out T pMsg, Stream stream)
    {
        try
        {
            pMsg = ProtoBuf.Serializer.Deserialize<T>(stream);
            if (null == pMsg)
            {
                Debug.LogError("Proto解析为Null");
                pMsg = default(T);
                return false;
            }
            return true;
        }
        catch (Exception)
        {
            Debug.LogError("Proto解析异常");
            pMsg = default(T);
            return false;
        }
    }
}

