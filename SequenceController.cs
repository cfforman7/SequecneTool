using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

/// <summary>
/// 시퀀스 소스 정보 클래스
/// </summary>
[Serializable]
public class SequenceSourceInfo
{
    [SerializeField]
    private RectTransform mRectTr;
    public RectTransform RectTr { get { return mRectTr; } }

    [SerializeField]
    private eSequenceActionType mActionType = eSequenceActionType.Move;
    public eSequenceActionType ActionType { get { return mActionType; } }
    [SerializeField]
    [Tooltip("추가 : Append 마지막에 추가" + "삽입 : Insert(float 삽입시간) 순서와 관계없이 일정 지점에 시작" + "결합 : Join 앞에 추가된 트윈과 동시 시작" + "접두 : Prepend 맨 처음에 추가")]
    private eSequenceAddType mSequenceType = eSequenceAddType.APPEND;
    public eSequenceAddType SequenceType { get { return mSequenceType; } }

    [SerializeField]
    private Ease mEaseType = Ease.Linear;
    public Ease EaseType { get { return mEaseType; } }

    [SerializeField]
    private float mDuration;
    public float DurationTime { get { return mDuration; } }

    [SerializeField]
    private float mDelayTime;
    public float DelayTime { get { return mDelayTime; } }

    [SerializeField]
    private float mInsertTime;
    public float InsertTime { get { return mInsertTime; } }

    [SerializeField]
    private bool mIsSetFrom;
    public bool IsSetFrom { get { return mIsSetFrom; } }

    [SerializeField]
    private Vector3 mFromVec3 = Vector3.zero;
    public Vector3 FromVec3 { get { return mFromVec3; } }

    [SerializeField]
    private Vector3 mToVec3 = Vector3.zero;
    public Vector3 ToVec3 { get { return mToVec3; } }

    [SerializeField]
    private Vector3 mFromVec2 = Vector3.zero;
    public Vector2 FromVec2 { get { return mFromVec2; } }

    [SerializeField]
    private Vector3 mToVec2 = Vector3.zero;
    public Vector2 ToVec2 { get { return mToVec2; } }

    [SerializeField]
    private Color mFromColor = Color.white;
    public Color FromColor { get { return mFromColor; } }

    [SerializeField]
    private Color mToColor = Color.white;
    public Color ToColor { get { return mToColor; } }

    [SerializeField]
    private float mFromAlpha;
    public float FromAlpha { get { return mFromAlpha; } }

    [SerializeField]
    private float mToAlpha;
    public float ToAlpha { get { return mToAlpha; } }

    [SerializeField]
    private string mEndCallback;
    public string EndCallback { get { return mEndCallback; } }

    [SerializeField]
    private ParticleSystem mParticle;
    public ParticleSystem Particle { get { return mParticle; } }

    [SerializeField]
    private eParticleCallbackType mParticleCallbackType;
    public eParticleCallbackType ParticleCallbackType { get { return mParticleCallbackType; } }


    [SerializeField]
    private eSequenceParticleEvent mParticlePlayType;
    public eSequenceParticleEvent ParticlePlayType { get { return mParticlePlayType; } }

    public SequenceSourceInfo(RectTransform aRt, eSequenceActionType aActionType, eSequenceAddType aAddType, Ease aEase, bool aIsSetFrom,
        Vector3 aFrom3, Vector3 aTo3, Color aFromCol, Color aToCol, Vector2 aFrom2, Vector2 aTo2,
        float aFromAlpha, float aToAlpha, float aDu, float aDelay, float aInsert, eSequenceParticleEvent aParticleType, eParticleCallbackType aParticleCallback, ParticleSystem aParticle = null) //string aEndCallback)
    {
        mRectTr = aRt;
        mActionType = aActionType;
        mSequenceType = aAddType;
        mEaseType = aEase;
        mIsSetFrom = aIsSetFrom;
        mFromVec3 = aFrom3;
        mToVec3 = aTo3;
        mFromColor = aFromCol;
        mToColor = aToCol;
        mFromVec2 = aFrom2;
        mToVec2 = aTo2;
        mFromAlpha = aFromAlpha;
        mToAlpha = aToAlpha;
        mDuration = aDu;
        mDelayTime = aDelay;
        mInsertTime = 0f;
        if (aAddType == eSequenceAddType.INSERT)
        {
            mInsertTime = aInsert;
        }
        //mEndCallback = aEndCallback;
        mParticle = aParticle;
        mParticlePlayType = aParticleType;
        mParticleCallbackType = aParticleCallback;
    }

    /// <summary>
    /// From 값으로 셋팅
    /// </summary>
    public void SettingUIByFromValue()
    {
        switch (mActionType)
        {
            case eSequenceActionType.Color:
                {
                    Image image = mRectTr.GetComponent<Image>();
                    if (image != null)
                    {
                        mRectTr.GetComponent<Image>().color = mFromColor;
                    }
                }
                break;

            case eSequenceActionType.Fade:
                {
                    //CanvasGroup canvasGroup = mRectTr.GetComponent<CanvasGroup>();
                    //if (canvasGroup != null)
                    //{
                    //    canvasGroup.alpha = mFromAlpha;
                    //}
                }
                break;

            case eSequenceActionType.LocalMove:
                {
                    //Vector3 vLocalPos = new Vector3(mRectTr.localPosition.x + mFromVec3.x, mRectTr.localPosition.y + mFromVec3.y + mRectTr.localPosition.z + mFromVec3.z);
                    //mRectTr.localPosition = vLocalPos;
                }
                break;

            case eSequenceActionType.LocalRotate:
                {
                    //Vector3 vLocalRot = new Vector3(mRectTr.localRotation.x + mFromVec3.x, mRectTr.localRotation.y + mFromVec3.y + mRectTr.localRotation.z + mFromVec3.z);
                    //mRectTr.localRotation = Quaternion.Euler(vLocalRot);
                }
                break;
            case eSequenceActionType.Move:
                mRectTr.position = mFromVec3; break;
            case eSequenceActionType.Rotate:
                mRectTr.rotation = Quaternion.Euler(mFromVec3); break;
            case eSequenceActionType.Scale:
                mRectTr.localScale = mFromVec3; break;
            case eSequenceActionType.UIWidthHeight:
                mRectTr.sizeDelta = mFromVec2; break;

        }
    }

    /// <summary>
    /// From 값으로 셋팅
    /// </summary>
    public void InitFromValue()
    {
        switch (mActionType)
        {
            case eSequenceActionType.Color:
                {
                    Image image = mRectTr.GetComponent<Image>();
                    if (image != null)
                    {
                        mRectTr.GetComponent<Image>().color = mFromColor;
                    }
                }
                break;

            case eSequenceActionType.Fade:
                {
                    CanvasGroup canvasGroup = mRectTr.GetComponent<CanvasGroup>();
                    if (canvasGroup != null)
                    {
                        canvasGroup.alpha = mFromAlpha;
                    }
                }
                break;
            case eSequenceActionType.Move:
                mRectTr.position = mFromVec3; break;
            case eSequenceActionType.Rotate:
                mRectTr.rotation = Quaternion.Euler(mFromVec3); break;
            case eSequenceActionType.Scale:
                mRectTr.localScale = mFromVec3; break;
            case eSequenceActionType.UIWidthHeight:
                mRectTr.sizeDelta = mFromVec2; break;
        }
    }
}

/// <summary>
/// 시퀀스 정보 클래스
/// </summary>
[Serializable]
public class PreviewSequenceInfo
{
    [SerializeField]
    public string Key;
    [SerializeField]
    public List<SequenceSourceInfo> SourceList;
    [SerializeField]
    public bool IsBackWard;
    public PreviewSequenceInfo(string aKey, List<SequenceSourceInfo> aList)
    {
        Key = aKey;
        SourceList = aList;
    }

    /// <summary>
    /// 소스 정보 추가
    /// </summary>
    /// <param name="aInfo"></param>
    public void AddSourceInfo(SequenceSourceInfo aInfo)
    {
        SourceList.Add(aInfo);
    }

    /// <summary>
    /// 소스 정보 갱신
    /// </summary>
    public void RefreshSourceInfo(int aIndex, SequenceSourceInfo aInfo)
    {
        SourceList[aIndex] = aInfo;
    }

    /// <summary>
    /// 소스 정보 삭제
    /// </summary>
    public void DeleteSourceInfo(int aIndex)
    {
        SourceList.RemoveAt(aIndex);
    }

    /// <summary>
    /// 소스 정보 해당 인덱스로 이동
    /// </summary>
    public void ChangePositionSourceInfo(int aOrigin, int aMove, SequenceSourceInfo aInfo)
    {
        SequenceSourceInfo origin = SourceList[aOrigin];
        SourceList[aOrigin] = SourceList[aMove];
        SourceList[aMove] = origin;
    }

    /// <summary>
    /// UI RectTransform을 저장한다.
    /// </summary>
    public void SaveRectTransformByUISelf()
    {
        if (SourceList == null) return;
        for (int i = 0; i < SourceList.Count; i++)
        {
            SequenceStuffObjectInfo sInfo = SourceList[i].RectTr.gameObject.GetComponent<SequenceStuffObjectInfo>();
            if (sInfo == null)
            {
                sInfo = SourceList[i].RectTr.gameObject.AddComponent<SequenceStuffObjectInfo>();
            }
            sInfo.SaveRectTransformInfo(SourceList[i].RectTr);
        }
    }

    /// <summary>
    /// UI를 저장된 Rect 정보로 로드한다.
    /// </summary>
    public void LoadRectTransformBySavedInfo()
    {
        if (SourceList == null) return;
        for (int i = 0; i < SourceList.Count; i++)
        {
            SequenceStuffObjectInfo sInfo = SourceList[i].RectTr.gameObject.GetComponent<SequenceStuffObjectInfo>();
            if (sInfo == null)
            {
                //LogConsole.Err("Not Found SequenceStuffInfo");
                return;
            }
            sInfo.LoadRectTransformBySavedInfo();
        }
    }

    /// <summary>
    /// 연출 시작 전 From 값으로 셋팅한다.
    /// </summary>
    public void InitializeSourceUIByFromValue()
    {
        if (SourceList == null) return;
        Dictionary<RectTransform, List<eSequenceActionType>> dic = new Dictionary<RectTransform, List<eSequenceActionType>>();
        for (int i = 0; i < SourceList.Count; i++)
        {
            if (dic.ContainsKey(SourceList[i].RectTr) == false)
            {
                SourceList[i].InitFromValue();
                List<eSequenceActionType> list = new List<eSequenceActionType>();
                list.Add(SourceList[i].ActionType);
                dic.Add(SourceList[i].RectTr, list);
            }
            else
            {
                if (dic[SourceList[i].RectTr].Contains(SourceList[i].ActionType) == false)
                {
                    SourceList[i].InitFromValue();
                    dic[SourceList[i].RectTr].Add(SourceList[i].ActionType);
                }
            }
        }
    }
}

/// <summary>
/// 시퀀스 컨트롤러
/// </summary>
[ExecuteInEditMode]
public class SequenceController : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    public List<PreviewSequenceInfo> PreviewSeqList;
    private Dictionary<string, Sequence> SequenceDictionary;
    //private Dictionary<string, OnSequencePlayCallback> sequencePlayCallbackDic;
    private Sequence mSequence;

    public delegate void TweenCompleteCallback(string aMethodName);
    public TweenCompleteCallback OnCompleteCallback { get; set; }

    public delegate void OnSequencePlayCallback(string key);
    public OnSequencePlayCallback OnPlay { get; set; }
    private void Awake()
    {
#if UNITY_EDITOR
        if (Application.isPlaying == false)
        {
            string sPath = UnityEditor.PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
            UnityEditor.EditorPrefs.SetString(gameObject.GetInstanceID().ToString(), sPath);
        }
#endif
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            UnityEditor.EditorPrefs.DeleteKey(gameObject.GetInstanceID().ToString());
        }
#endif
    }

    //public void RegistSequencePlayCallback(string key, OnSequencePlayCallback call)
    //{
    //    if(sequencePlayCallbackDic == null)
    //    {
    //        sequencePlayCallbackDic = new Dictionary<string, OnSequencePlayCallback>();
    //    }
    //    if(!sequencePlayCallbackDic.ContainsKey(key))
    //    {
    //        sequencePlayCallbackDic.Add(key, call);
    //    }
    //    else
    //    {
    //        sequencePlayCallbackDic[key] += call;
    //    }
    //}

    //public void RemoveSequencePlayCallback(string key, OnSequencePlayCallback call)
    //{
    //    if (sequencePlayCallbackDic == null) return;

    //    if (sequencePlayCallbackDic.ContainsKey(key))
    //    {
    //        sequencePlayCallbackDic[key] -= call;
    //        if (sequencePlayCallbackDic[key] == null)
    //        {
    //            sequencePlayCallbackDic.Remove(key);
    //        }
    //    }
    //}

    /// <summary>
    /// 시퀀스 정보를 저장한다
    /// </summary>
    public void SettingPreviewSequenceInfo(PreviewSequenceInfo aInfo)
    {
        if (PreviewSeqList == null)
        {
            PreviewSeqList = new List<PreviewSequenceInfo>();
        }
        PreviewSeqList.Add(aInfo);
    }

    /// <summary>
    /// 시퀀스 정보 갱신
    /// </summary>
    public void RefreshPreviewSequenceInfo(string aKey, PreviewSequenceInfo aInfo)
    {
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == aKey)
            {
                PreviewSeqList[i] = aInfo;
            }
        }
    }

    /// <summary>
    /// 시퀀스 소스 정보 갱신
    /// </summary>
    public void RefreshPreviewSequenceSourceInfo(string aKey, int aIndex, SequenceSourceInfo aInfo)
    {
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == aKey)
            {
                PreviewSeqList[i].RefreshSourceInfo(aIndex, aInfo);
                break;
            }
        }
    }

    /// <summary>
    /// 시퀀스 정보 삭제
    /// </summary>
    public void DeletePreviewSeqeunceInfo(string aKey)
    {
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == aKey)
            {
                PreviewSeqList.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// 해당 Key의 시퀀스 정보 리턴.
    /// </summary>
    public PreviewSequenceInfo GetPreviewSequenceInfoByKey(string aKey)
    {
        if (PreviewSeqList == null) return null;
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == aKey)
            {
                return PreviewSeqList[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 키 존재 유무
    /// </summary>
    public bool HasKey(string key)
    {
        if (PreviewSeqList == null) return false;
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == key)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 새로운 시퀀스 키로 복사
    /// </summary>
    public void CopyPreviewSequenceInfo(string aCopyKey, string aNewKey)
    {
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == aCopyKey)
            {
                List<SequenceSourceInfo> list = new List<SequenceSourceInfo>();
                list.AddRange(PreviewSeqList[i].SourceList);
                PreviewSequenceInfo pInfo = new PreviewSequenceInfo(aNewKey, list);
                PreviewSeqList.Add(pInfo);
                break;
            }
        }
    }

    /// <summary>
    /// 시퀀스 소스 위치 변경
    /// </summary>
    public void ChangePositionSourceInfo(string aKey, int aOriginIndex, int aMoveIndex, SequenceSourceInfo aTarget)
    {
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == aKey)
            {
                PreviewSeqList[i].ChangePositionSourceInfo(aOriginIndex, aMoveIndex, aTarget);
            }
        }
    }

    /// <summary>
    /// 시퀀스 재생
    /// </summary>
    public void PlaySequenceByKey(string aKey, bool aIsReset = false, Action aEndCall = null)
    {
        if (SequenceDictionary == null)
        {
            SequenceDictionary = new Dictionary<string, Sequence>();
        }

        PreviewSequenceInfo pInfo = GetPreviewSequenceInfoByKey(aKey);
        if (pInfo == null) return;
        if (SequenceDictionary.ContainsKey(aKey))
        {
            Sequence seq = SequenceDictionary[aKey];
            if (seq != null && seq.IsPlaying())
            {
                seq.Restart();
                seq.OnComplete(
                    () =>
                    {
                        aEndCall?.Invoke();
                        SequenceDictionary.Remove(seq.stringId);
                    });
                return;
            }
        }
        if (aIsReset)
        {
            //UI 완전 초기값 셋팅
            pInfo.LoadRectTransformBySavedInfo();            
        }
        Sequence aSequence = DOTween.Sequence();
        aSequence.stringId = aKey;
        if (SequenceDictionary.ContainsKey(aKey) == false)
        {
            SequenceDictionary.Add(aKey, aSequence);
        }
        for (int i = 0; i < pInfo.SourceList.Count; i++)
        {
            SequenceSourceInfo sInfo = pInfo.SourceList[i];
            if (sInfo.IsSetFrom)
            {
                sInfo.SettingUIByFromValue();
            }
            var group = sInfo.RectTr.GetComponent<CanvasGroup>();
            if(group != null)
            {
                groupList.Add(group);
            }
            eSequenceActionType aType = pInfo.SourceList[i].ActionType;
            switch (pInfo.SourceList[i].SequenceType)
            {
                case eSequenceAddType.APPEND:
                    aSequence.Append(MakeTween(aType, sInfo, pInfo.IsBackWard)); break;
                case eSequenceAddType.INSERT:
                    aSequence.Insert(pInfo.SourceList[i].InsertTime, MakeTween(aType, sInfo, pInfo.IsBackWard)); break;
                case eSequenceAddType.JOIN:
                    aSequence.Join(MakeTween(aType, sInfo, pInfo.IsBackWard)); break;
                case eSequenceAddType.PREPEND:
                    aSequence.Prepend(MakeTween(aType, sInfo, pInfo.IsBackWard)); break;
            }
        }
        aSequence.OnComplete(
                    () =>
                    {
                        aEndCall?.Invoke();
                        SequenceDictionary.Remove(aSequence.stringId);
                    });
        aSequence.Play();

        OnPlay?.Invoke(aKey);
        //if(sequencePlayCallbackDic != null && sequencePlayCallbackDic.ContainsKey(aKey))
        //{
        //    sequencePlayCallbackDic[aKey]();
        //}
    }

    private List<CanvasGroup> groupList = new List<CanvasGroup>();

    /// <summary>
    /// 시퀀스 소스 Tween으로 만들기
    /// </summary>
    public Tween MakeTween(eSequenceActionType aActionType, SequenceSourceInfo aInfo, bool aIsBackWard)
    {
        switch (aActionType)
        {
            case eSequenceActionType.Fade:
                var canvasGroup = aInfo.RectTr.GetComponent<CanvasGroup>();
                if (canvasGroup == null) return null;

                return canvasGroup.DOFade(aInfo.ToAlpha, aInfo.DurationTime).SetInverted(aIsBackWard)
                    .From(aInfo.FromAlpha, false).SetDelay(aInfo.DelayTime).SetEase(aInfo.EaseType)
                    .OnStart(
                    () =>
                    {
                        if (aInfo.IsSetFrom)
                            canvasGroup.alpha = aInfo.FromAlpha;

                        if (aInfo.Particle != null && aInfo.ParticleCallbackType == eParticleCallbackType.ON_START)
                        {
                            PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                        }
                    })
                    .OnComplete(
                        () =>
                        {
                            if (aInfo.Particle != null && aInfo.ParticleCallbackType == eParticleCallbackType.ON_COMPLETE)
                            {
                                PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                            }
                        });
            case eSequenceActionType.Color:
                return aInfo.RectTr.GetComponent<Image>().DOColor(aInfo.ToColor, aInfo.DurationTime).SetInverted(aIsBackWard)
                    .From(aInfo.FromColor, false).SetDelay(aInfo.DelayTime).SetEase(aInfo.EaseType)
                    .OnStart(
                    () =>
                    {
                        if (aInfo.Particle != null && aInfo.ParticleCallbackType == eParticleCallbackType.ON_START)
                        {
                            PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                        }
                    })
                    .OnComplete(
                        () =>
                        {
                            if (aInfo.Particle != null)
                            {
                                PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                            }
                        });
            case eSequenceActionType.LocalMove:
                {
                    Vector3 vFrom = aInfo.RectTr.localPosition + aInfo.FromVec3;
                    Vector3 vTo = aInfo.RectTr.localPosition + aInfo.ToVec3;

                    return aInfo.RectTr.DOLocalMove(vTo, aInfo.DurationTime).SetInverted(aIsBackWard)
                        .From(vFrom, false).SetDelay(aInfo.DelayTime).SetEase(aInfo.EaseType).SetRelative(true)
                        .OnStart(
                        () =>
                        {
                            if (aInfo.Particle != null && aInfo.ParticleCallbackType == eParticleCallbackType.ON_START)
                            {
                                PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                            }
                        })
                        .OnComplete(
                        () =>
                        {
                            if (aInfo.Particle != null)
                            {
                                PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                            }
                        });
                }
            case eSequenceActionType.LocalRotate:
                {
                    Vector3 vFrom = aInfo.RectTr.localRotation.eulerAngles + aInfo.FromVec3;
                    Vector3 vTo = aInfo.RectTr.localRotation.eulerAngles + aInfo.ToVec3;

                    return aInfo.RectTr.DOLocalRotate(vTo, aInfo.DurationTime).SetInverted(aIsBackWard)
                        .From(vFrom, false, true).SetDelay(aInfo.DelayTime).SetEase(aInfo.EaseType).SetRelative(true)
                        .OnStart(
                        () =>
                        {
                            if (aInfo.Particle != null && aInfo.ParticleCallbackType == eParticleCallbackType.ON_START)
                            {
                                PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                            }
                        })
                        .OnComplete(
                        () =>
                        {
                            if (aInfo.Particle != null)
                            {
                                PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                            }
                        });
                }
            case eSequenceActionType.Move:
                return aInfo.RectTr.DOMove(aInfo.ToVec3, aInfo.DurationTime).SetInverted(aIsBackWard)
                    .From(aInfo.FromVec3).SetDelay(aInfo.DelayTime).SetEase(aInfo.EaseType)
                    .OnStart(
                    () =>
                    {
                        if (aInfo.Particle != null && aInfo.ParticleCallbackType == eParticleCallbackType.ON_START)
                        {
                            PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                        }
                    })
                    .OnComplete(
                        () =>
                        {
                            if (aInfo.Particle != null)
                            {
                                PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                            }
                        });
            case eSequenceActionType.Rotate:
                return aInfo.RectTr.DORotate(aInfo.ToVec3, aInfo.DurationTime).SetInverted(aIsBackWard)
                    .From(aInfo.FromVec3).SetDelay(aInfo.DelayTime).SetEase(aInfo.EaseType)
                    .OnStart(
                    () =>
                    {
                        if (aInfo.Particle != null && aInfo.ParticleCallbackType == eParticleCallbackType.ON_START)
                        {
                            PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                        }
                    })
                    .OnComplete(
                        () =>
                        {
                            if (aInfo.Particle != null)
                            {
                                PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                            }
                        });
            case eSequenceActionType.Scale:
                return aInfo.RectTr.DOScale(aInfo.ToVec3, aInfo.DurationTime).SetInverted(aIsBackWard)
                    .From(aInfo.FromVec3, false).SetDelay(aInfo.DelayTime).SetEase(aInfo.EaseType)
                    .OnStart(
                    () =>
                    {
                        if (aInfo.Particle != null && aInfo.ParticleCallbackType == eParticleCallbackType.ON_START)
                        {
                            PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                        }
                    })
                    .OnComplete(
                        () =>
                        {
                            if (aInfo.Particle != null)
                            {
                                PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                            }
                        });
            case eSequenceActionType.UIWidthHeight:
                return aInfo.RectTr.DOSizeDelta(aInfo.ToVec2, aInfo.DurationTime).SetInverted(aIsBackWard)
                    .From(aInfo.FromVec2).SetDelay(aInfo.DelayTime).SetEase(aInfo.EaseType)
                    .OnStart(
                    () =>
                    {
                        if (aInfo.Particle != null && aInfo.ParticleCallbackType == eParticleCallbackType.ON_START)
                        {
                            PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                        }
                    })
                    .OnComplete(
                        () =>
                        {
                            if (aInfo.Particle != null)
                            {
                                PlayParticleCallback(aInfo.Particle, aInfo.ParticlePlayType);
                            }
                        });
        }
        return null;
    }

    /// <summary>
    /// 모든 RectTransform의 초기 값을 저장한다
    /// </summary>
    public void SaveAllRectTransform()
    {
        if (PreviewSeqList == null) return;
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            PreviewSequenceInfo pInfo = PreviewSeqList[i];
            pInfo.SaveRectTransformByUISelf();
        }
    }

    /// <summary>
    /// RectTransform을 저장한다
    /// </summary>
    public void SaveRectTransform(string aKey)
    {
        if (PreviewSeqList == null) return;
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == aKey)
            {
                PreviewSequenceInfo pInfo = PreviewSeqList[i];
                pInfo.SaveRectTransformByUISelf();
                break;
            }
        }
    }

    /// <summary>
    /// 소스 오브젝트의 UI 초기값으로 되돌린다.
    /// </summary>
    public void LoadRectTransformBySavedInfo(string aKey)
    {
        if (PreviewSeqList == null) return;
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == aKey)
            {
                PreviewSequenceInfo pInfo = PreviewSeqList[i];
                pInfo.LoadRectTransformBySavedInfo();
                break;
            }
        }
    }

    /// <summary>
    /// 모든 시퀀스 소스 오브젝트의 UI 초기값으로 되돌린다.
    /// </summary>
    public void AllLoadRectTransformBySavedInfo()
    {
        if (PreviewSeqList == null) return;
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            PreviewSequenceInfo pInfo = PreviewSeqList[i];
            pInfo.LoadRectTransformBySavedInfo();
        }
    }

    /// <summary>
    /// 해당 키에 관련된 소스들의 From 값으로 설정한다
    /// </summary>
    public void InitializeFromValueByKey(string aInitKeyName)
    {
        if (PreviewSeqList == null) return;
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == aInitKeyName)
            {
                PreviewSequenceInfo pInfo = PreviewSeqList[i];
                pInfo.InitializeSourceUIByFromValue();
                break;
            }
        }
    }

    /// <summary>
    /// 소스 오브젝트의 UI From 값으로셋팅한다.
    /// </summary>
    public void SettingSourceUIByFromData(string aKey)
    {
        if (PreviewSeqList == null) return;
        for (int i = 0; i < PreviewSeqList.Count; i++)
        {
            if (PreviewSeqList[i].Key == aKey)
            {
                PreviewSequenceInfo pInfo = PreviewSeqList[i];
                pInfo.InitializeSourceUIByFromValue();
                break;
            }
        }
    }

    public void CompleteCallback(string aMethodName)
    {
        OnCompleteCallback?.Invoke(aMethodName);
    }

    /// <summary>
    /// 파티클 콜백에 따른 프로세스
    /// </summary>    
    private void PlayParticleCallback(ParticleSystem aParticle, eSequenceParticleEvent aPlayType)
    {
        switch (aPlayType)
        {
            case eSequenceParticleEvent.INACTIVE:
                aParticle.gameObject.SetActive(false);
                aParticle.Stop(true);
                break;
            case eSequenceParticleEvent.ACTIVE:
                aParticle.gameObject.SetActive(true);
                aParticle.Play(true);
                break;
            case eSequenceParticleEvent.PLAY:
                aParticle.Play(true); break;
            case eSequenceParticleEvent.STOP:
                aParticle.Stop(true); break;
        }
    }

    /// <summary>
    /// 시퀀스의 총 재생시간 리턴
    /// </summary>
    public float GetDuration(string aKey)
    {
        PreviewSequenceInfo pInfo = GetPreviewSequenceInfoByKey(aKey);
        if (pInfo == null) return - 1f;
        if (SequenceDictionary.ContainsKey(aKey))
        {
            Sequence seq = SequenceDictionary[aKey];
            return seq.Duration();
        }
        return -1f;
    }
}
