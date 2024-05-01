#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Shape
{
	public Vector3 startScale;

	// lewy przyscisk myszu
	public bool playingAnimationLMB;
	// prawy przycisk myszy
	public bool playingAnimationRMB;
	// ult
	public bool playingAnimationR;

	public float cooldownLMB;
	public float cooldownRMB;
	public float cooldownR;

	public float animationTimeLMB;

	public float projectileVelocity;
	public float attackDistance;
	public float damage;
	protected BulletShooterType bulletShooterType;

	protected float attackSpeed = 1;

	public AudioClip? audioClip;
	public Shape(BulletShooterType shooterType, AudioClip? audioClip = null)
	{
		this.playingAnimationLMB = false;
		this.playingAnimationRMB = false;
		this.playingAnimationR = false;

		this.cooldownLMB = 0;
		this.cooldownRMB = 0;
		this.cooldownR = 0;

		this.animationTimeLMB = 1;

		this.projectileVelocity = 16f;

		this.bulletShooterType = shooterType;
		this.audioClip = audioClip;
	}
	public bool IsPlayingAnimations()
	{
		if (this.playingAnimationLMB)
		{
			return true;
		}
		if (this.playingAnimationRMB)
		{
			return true;
		}
		if (this.playingAnimationR)
		{
			return true;
		}
		return false;
	}
	public virtual float CalcDamage(float raw)
	{
		return raw;
	}
	public virtual void OnChange(SpriteRenderer spriteRenderer)
	{
		Debug.Log(Resources.Load<Sprite>("TriangleSceneSprite"));
		
		//Texture2D tex = Resources.Load<Texture2D>("TrangleShapeSprite");
		//spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, 128, 128), spriteRenderer.sprite.pivot);

		spriteRenderer.sprite = Resources.Load<Sprite>("TriangleSceneSprite");
		//spriteRenderer.sprite = Sprite.Create()
		// dodać tekstury odpowiednich kształtów
		//spriteRenderer.sprite = Sprite.Create()
	}
	public virtual void OnEnd(Rigidbody2D player)
	{
		player.transform.localScale = this.startScale;
	}
	public virtual void OnTick(float dt, Rigidbody2D rigitBody2D)
	{ 
		this.cooldownLMB -= dt;
		this.cooldownRMB -= dt;
		this.cooldownR -= dt;

		if (cooldownLMB <= 0)
		{
			cooldownLMB = 0;
		}
		if (cooldownRMB <= 0)
		{
			cooldownRMB = 0;
		}
		if (cooldownR <= 0)
		{
			cooldownR = 0;
		}

		if (this.playingAnimationLMB)
		{
			this.AnimationLeftMouseButton(dt, rigitBody2D);
		}
		if (this.playingAnimationRMB)
		{
			this.AnimationRightMouseButton(dt);
		}
		if (this.playingAnimationR)
		{
			this.AnimationR(dt);
		}
	}
	public virtual void OnLeftMouseButton(float dt, Rigidbody2D ownerRigitbody, Rigidbody2D bulletPattern, Rigidbody2D squeareBulletPattern, AudioSource? audioSource = null)
	{
		if (this.cooldownLMB <= 0)
		{
			if (!this.playingAnimationLMB)
			{
				this.startScale = ownerRigitbody.transform.localScale;
			}
			this.cooldownLMB = this.attackSpeed;
			this.playingAnimationLMB = true;
			this.animationTimeLMB = 0;
			if (audioSource != null)
			{
                audioSource.PlayOneShot(this.audioClip);
            }
			this.ActionLeftMouseButton(ownerRigitbody, bulletPattern, squeareBulletPattern);
		}
	}
	public virtual void OnRightMouseButton(float dt)
	{
		if (this.cooldownLMB <= 0)
		{
			this.ActionRightMouseButton();
			this.cooldownRMB = 5;
			this.playingAnimationRMB = true;
		}
	}
	public virtual void OnRButton(float dt)
	{
		if (this.cooldownLMB <= 0)
		{
			this.ActionR();
			this.cooldownR = 10;
			this.playingAnimationR = true;
		}
	}
	public virtual void ActionLeftMouseButton(Rigidbody2D player, Rigidbody2D bulletPattern, Rigidbody2D squeareBulletPattern)
	{
		
	}
	public virtual void ActionRightMouseButton()
	{

	}
	public virtual void ActionR()
	{

	}
	public virtual void AnimationLeftMouseButton(float dt, Rigidbody2D rigidbody2D)
	{
		// jeżeli zakończono animację, należy zmienić playingAnimation na false
		this.animationTimeLMB += dt * 5f;
		if (this.animationTimeLMB >= 1)
		{
			this.animationTimeLMB = 1;
			this.playingAnimationLMB = false;
		}
	}
	public virtual void AnimationRightMouseButton(float dt)
	{

	}
	public virtual void AnimationR(float dt)
	{

	}
	public virtual float CalculateAnimationLMB()
	{
		return 1;
	}
	public virtual void TakeDamage()
	{

	}
}
