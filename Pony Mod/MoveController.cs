using GlobalEnums;
using UnityEngine;
/*
namespace PonyMod
{
    public class MoveController
    {
        public void FixedUpdate(HeroController knight)
        {
            if (knight.cState.recoilingLeft || knight.cState.recoilingRight)
            {
                if ((float) recoilSteps <= RECOIL_HOR_STEPS)
                {
                    recoilSteps++;
                }
                else
                {
                    CancelRecoilHorizontal();
                }
            }

            if (knight.cState.dead)
            {
                rb2d.velocity = new Vector2(0f, 0f);
            }

            if ((knight.hero_state == ActorStates.hard_landing && !knight.cState.onConveyor) || knight.hero_state == ActorStates.dash_landing)
            {
                ResetMotion();
            }
            else if (knight.hero_state == ActorStates.no_input)
            {
                if (knight.cState.transitioning)
                {
                    if (knight.transitionState == Heroknight.knight.transitionState.EXITING_SCENE)
                    {
                        AffectedByGravity(gravityApplies: false);
                        if (!stopWalkingOut)
                        {
                            rb2d.velocity = new Vector2(transition_vel.x, transition_vel.y + rb2d.velocity.y);
                        }
                    }
                    else if (knight.transitionState == HeroTransitionState.ENTERING_SCENE)
                    {
                        rb2d.velocity = transition_vel;
                    }
                    else if (knight.transitionState == HeroTransitionState.DROPPING_DOWN)
                    {
                        rb2d.velocity = new Vector2(transition_vel.x, rb2d.velocity.y);
                    }
                }
                else if (knight.cState.recoiling)
                {
                    knight.AffectedByGravity(gravityApplies: false);
                    rb2d.velocity = recoilVector;
                }
            }
            else if (knight.hero_state != ActorStates.no_input)
            {
                if (knight.hero_state == ActorStates.running)
                {
                    if (move_input > 0f)
                    {
                        if (CheckForBump(CollisionSide.right))
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x, BUMP_VELOCITY);
                        }
                    }
                    else if (move_input < 0f && CheckForBump(CollisionSide.left))
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, BUMP_VELOCITY);
                    }
                }

                if (!knight.cState.backDashing && !knight.cState.dashing)
                {
                    Move(move_input);
                    if ((!knight.cState.attacking || !(attack_time < ATTACK_RECOVERY_TIME)) && !knight.cState.wallSliding && !wallLocked)
                    {
                        if (move_input > 0f && !knight.cState.facingRight)
                        {
                            FlipSprite();
                            CancelAttack();
                        }
                        else if (move_input < 0f && knight.cState.facingRight)
                        {
                            FlipSprite();
                            CancelAttack();
                        }
                    }

                    if (knight.cState.recoilingLeft)
                    {
                        float num = ((!recoilLarge) ? RECOIL_HOR_VELOCITY : RECOIL_HOR_VELOCITY_LONG);
                        if (rb2d.velocity.x > 0f - num)
                        {
                            rb2d.velocity = new Vector2(0f - num, rb2d.velocity.y);
                        }
                        else
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x - num, rb2d.velocity.y);
                        }
                    }

                    if (knight.cState.recoilingRight)
                    {
                        float num2 = ((!recoilLarge) ? RECOIL_HOR_VELOCITY : RECOIL_HOR_VELOCITY_LONG);
                        if (rb2d.velocity.x < num2)
                        {
                            rb2d.velocity = new Vector2(num2, rb2d.velocity.y);
                        }
                        else
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x + num2, rb2d.velocity.y);
                        }
                    }
                }

                if ((knight.cState.lookingUp || knight.cState.lookingDown) && Mathf.Abs(move_input) > 0.6f)
                {
                    ResetLook();
                }

                if (knight.cState.jumping)
                {
                    Jump();
                }

                if (knight.cState.doubleJumping)
                {
                    DoubleJump();
                }

                if (knight.cState.dashing)
                {
                    Dash();
                }

                if (knight.cState.casting)
                {
                    if (knight.cState.castRecoiling)
                    {
                        if (knight.cState.facingRight)
                        {
                            rb2d.velocity = new Vector2(0f - CAST_RECOIL_VELOCITY, 0f);
                        }
                        else
                        {
                            rb2d.velocity = new Vector2(CAST_RECOIL_VELOCITY, 0f);
                        }
                    }
                    else
                    {
                        rb2d.velocity = Vector2.zero;
                    }
                }

                if (knight.cState.bouncing)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, BOUNCE_VELOCITY);
                }

                _ = knight.cState.shroomBouncing;
                if (wallLocked)
                {
                    if (wallJumpedR)
                    {
                        rb2d.velocity = new Vector2(currentWalljumpSpeed, rb2d.velocity.y);
                    }
                    else if (wallJumpedL)
                    {
                        rb2d.velocity = new Vector2(0f - currentWalljumpSpeed, rb2d.velocity.y);
                    }

                    wallLockSteps++;
                    if (wallLockSteps > WJLOCK_STEPS_LONG)
                    {
                        wallLocked = false;
                    }

                    currentWalljumpSpeed -= walljumpSpeedDecel;
                }

                if (knight.cState.wallSliding)
                {
                    if (wallSlidingL && inputHandler.inputActions.right.IsPressed)
                    {
                        wallUnstickSteps++;
                    }
                    else if (wallSlidingR && inputHandler.inputActions.left.IsPressed)
                    {
                        wallUnstickSteps++;
                    }
                    else
                    {
                        wallUnstickSteps = 0;
                    }

                    if (wallUnstickSteps >= WALL_STICKY_STEPS)
                    {
                        CancelWallsliding();
                    }

                    if (wallSlidingL)
                    {
                        if (!CheckStillTouchingWall(CollisionSide.left))
                        {
                            FlipSprite();
                            CancelWallsliding();
                        }
                    }
                    else if (wallSlidingR && !CheckStillTouchingWall(CollisionSide.right))
                    {
                        FlipSprite();
                        CancelWallsliding();
                    }
                }
            }

            if (rb2d.velocity.y < 0f - MAX_FALL_VELOCITY && !inAcid && !controlReqlinquished && !knight.cState.shadowDashing && !knight.cState.spellQuake)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f - MAX_FALL_VELOCITY);
            }

            if (jumpQueuing)
            {
                jumpQueueSteps++;
            }

            if (doubleJumpQueuing)
            {
                doubleJumpQueueSteps++;
            }

            if (dashQueuing)
            {
                dashQueueSteps++;
            }

            if (attackQueuing)
            {
                attackQueueSteps++;
            }

            if (knight.cState.wallSliding && !knight.cState.onConveyorV)
            {
                if (rb2d.velocity.y > WALLSLIDE_SPEED)
                {
                    rb2d.velocity = new Vector3(rb2d.velocity.x, rb2d.velocity.y - WALLSLIDE_DECEL);
                    if (rb2d.velocity.y < WALLSLIDE_SPEED)
                    {
                        rb2d.velocity = new Vector3(rb2d.velocity.x, WALLSLIDE_SPEED);
                    }
                }

                if (rb2d.velocity.y < WALLSLIDE_SPEED)
                {
                    rb2d.velocity = new Vector3(rb2d.velocity.x, rb2d.velocity.y + WALLSLIDE_DECEL);
                    if (rb2d.velocity.y < WALLSLIDE_SPEED)
                    {
                        rb2d.velocity = new Vector3(rb2d.velocity.x, WALLSLIDE_SPEED);
                    }
                }
            }

            if (nailArt_cyclone)
            {
                if (inputHandler.inputActions.right.IsPressed && !inputHandler.inputActions.left.IsPressed)
                {
                    rb2d.velocity = new Vector3(CYCLONE_HORIZONTAL_SPEED, rb2d.velocity.y);
                }
                else if (inputHandler.inputActions.left.IsPressed && !inputHandler.inputActions.right.IsPressed)
                {
                    rb2d.velocity = new Vector3(0f - CYCLONE_HORIZONTAL_SPEED, rb2d.velocity.y);
                }
                else
                {
                    rb2d.velocity = new Vector3(0f, rb2d.velocity.y);
                }
            }

            if (knight.cState.swimming)
            {
                rb2d.velocity = new Vector3(rb2d.velocity.x, rb2d.velocity.y + SWIM_ACCEL);
                if (rb2d.velocity.y > SWIM_MAX_SPEED)
                {
                    rb2d.velocity = new Vector3(rb2d.velocity.x, SWIM_MAX_SPEED);
                }
            }

            if (knight.cState.superDashOnWall && !knight.cState.onConveyorV)
            {
                rb2d.velocity = new Vector3(0f, 0f);
            }

            if (knight.cState.onConveyor && ((knight.cState.onGround && !knight.cState.superDashing) || knight.hero_state == ActorStates.hard_landing))
            {
                if (knight.cState.freezeCharge || knight.hero_state == ActorStates.hard_landing || controlReqlinquished)
                {
                    rb2d.velocity = new Vector3(0f, 0f);
                }

                rb2d.velocity = new Vector2(rb2d.velocity.x + conveyorSpeed, rb2d.velocity.y);
            }

            if (knight.cState.inConveyorZone)
            {
                if (knight.cState.freezeCharge || knight.hero_state == ActorStates.hard_landing)
                {
                    rb2d.velocity = new Vector3(0f, 0f);
                }

                rb2d.velocity = new Vector2(rb2d.velocity.x + conveyorSpeed, rb2d.velocity.y);
                superDash.SendEvent("SLOPE CANCEL");
            }

            if (knight.cState.slidingLeft && rb2d.velocity.x > -5f)
            {
                rb2d.velocity = new Vector2(-5f, rb2d.velocity.y);
            }

            if (landingBufferSteps > 0)
            {
                landingBufferSteps--;
            }

            if (ledgeBufferSteps > 0)
            {
                ledgeBufferSteps--;
            }

            if (headBumpSteps > 0)
            {
                headBumpSteps--;
            }

            if (jumpReleaseQueueSteps > 0)
            {
                jumpReleaseQueueSteps--;
            }

            positionHistory[1] = positionHistory[0];
            positionHistory[0] = transform.position;
            knight.cState.wasOnGround = knight.cState.onGround;
        }

    }
}*/
