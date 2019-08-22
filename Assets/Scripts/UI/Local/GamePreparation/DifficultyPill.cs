using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifficultyPill : InteractableMonoBehavior
{
    [GetComponent] public PulseElement pulseElement;
    [GetComponent] public CanvasGroup canvasGroup;
    [GetComponentInChildrenName("Background")] public GradientMeshEffect gradientMesh;
    [GetComponentInChildrenName("Name")] public Text name;
    [GetComponentInChildrenName("Level")] public Text level;

    private LevelMeta.ChartSection section;
    public Difficulty Difficulty { get; private set; }
    
    public void SetModel(LevelMeta.ChartSection section)
    {
        this.section = section;
        Difficulty = Difficulty.Parse(section.type);
        
        gradientMesh.SetGradient(Difficulty.Gradient);
        name.text = section.name ?? Difficulty.Name;
        level.text = "LV." + Difficulty.ConvertToDisplayLevel(section.difficulty);
    }

    private void Update()
    {
        if (Difficulty != null && Context.SelectedDifficulty == Difficulty)
        {
            canvasGroup.DOFade(1, 0.2f);
        }
        else
        {
            canvasGroup.DOFade(0.5f, 0.2f);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        transform.DOScale(0.9f, 0.2f).SetEase(Ease.OutCubic);
    }
        
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        transform.DOScale(1f, 0.2f).SetEase(Ease.OutCubic);
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        Select();
        Context.PreferredDifficulty = Difficulty;
    }

    public void Select(bool pulse = true)
    {
        Context.SelectedDifficulty = Difficulty;
        if (pulse) pulseElement.Pulse();
    }
    
}