using UnityEngine;
// Helps the Animation component (not the Animator component)
// allows you to animate an object even when Time.timeScale is set to 0 ( ie when the game is paused ).
// Just attach the script to the object you want to be animated, and it will always animate when enabled.
// NOTICE: any Animation component will be auto-deleted during Playmode, because we don't need them to sample animations :)
// However, to create animation clips, you might still need a temporary Animation component alongside with THIS component.
//
//https://forum.unity.com/threads/freebies-getting-back-on-track.107540/#post-2471016
public class AnimateEvenWhenPaused : MonoBehaviour
{

    // The local to our GameObject, holds the animation clips
    [SerializeField] private AnimationClip _playOnAwake_ifNotNull;
    // The current animation time. This is reset when starting a new animation
    float _animationTime = 0;
    bool _isAnimating = false;
    // a first ever update usually comes with a huge deltaTime, especially
    // in editor, so we can skip it:
    bool _skipNextUpdate = false;
    int _nextEvent_ix = 0;
    //will be invoked once, then set to null
    internal System.Action _on_animComplete = null;
    public System.Action _on_noAnimPlaying = null;
    public bool _playAutomatically
    {
        get { return _playOnAwake_ifNotNull != null; }
    }

    public AnimationClip clip { get; set; }
    float _playbackSpeed = 1.0f;
    public void setPlaybackSpeed(float speedMultiplier)
    {
        _playbackSpeed = speedMultiplier;
    }
    public bool isPlaying() { return _isAnimating; }
    public void Reset()
    {
        // "Animation' component is used to simplify creation of animations, during edit-mode.
        // It's destroyed during our Awake.
        gameObject.AddComponent<Animation>();
    }
    private void Awake()
    {
      //  Core.registerForUpdate(this, OnUpdate);//Auto-unregisters if this object will get destroyed
        if (_playOnAwake_ifNotNull != null)
        {
            clip = _playOnAwake_ifNotNull;
            // To first frame, in case if core's update already completed, and we won't
            // be animated until the next update:
            SetToFirstFrame();
            // launch:
            Play();
            // first ever update usually comes with a huge deltaTime,
            // especially in editor, so we skip it:
            _skipNextUpdate = true;
        }
        // All animations must instead happen through us;  Animation component was only used during edit-mode,
        // to simplify building specifying the parameters relative to our object, as we were animating inside editor.
        // Now, we don't needed it, so delete it:
        Animation localAnim = GetComponent<Animation>();
        //if (localAnim != null) { Destroy(localAnim); }
    }
    void OnUpdate()
    {
        if (_skipNextUpdate)
        {
            _skipNextUpdate = false;
            return;
        }
        // We are animating
        if (clip == null || _isAnimating == false)
        {
            _on_noAnimPlaying?.Invoke();
            return;
        }

        if (_animationTime < clip.length)
        {
            // Sample the animation from the time we set ( display the correct frame based on the animation time )
            _animationTime = Mathf.Min(_animationTime, clip.length);
            clip.SampleAnimation(gameObject, _animationTime);
            SampleEvents_ifNeeded();

            // Add to the animation time
            _animationTime += Time.unscaledDeltaTime * _playbackSpeed;
            return;
        }
        //else, reached the end of the clip, finish the animation:
        // Set the animation time to the length of the clip ( make sure we get to the end of the animation )
        _animationTime = clip.length;

        // Sample the animation from the time we set ( display the correct frame based on the animation time )
        clip.SampleAnimation(gameObject, _animationTime);
        SampleEvents_ifNeeded();

        if (clip.wrapMode == WrapMode.Loop)
        {
            _animationTime %= clip.length;
        }
        else
        {
            // We are not animating anymore
            _isAnimating = false;
            //clip = null; <---DON'T RESET TO NULL, - THE USER MIGHT WISH TO CALL Play()  LATER ON ON THE SAME CLIP
        }
        //notice, called even if we are Looping (and have just looped back to start):
        if (_on_animComplete != null)
        {
            System.Action ptr = _on_animComplete;
            _on_animComplete = null;//NOTICE - set to null BEFORE invoking - during invoking the user might re-subscribe.
            ptr.Invoke();
        }
    }//end ()
    void SampleEvents_ifNeeded()
    {
        //invoke all events whose time we've surpassed:
        while (true)
        {
            if (_nextEvent_ix >= clip.events.Length) { return; }

            AnimationEvent e = clip.events[_nextEvent_ix];
            if (_animationTime < e.time) { return; }
            // Ask to invoke this function on suitable Monobehaviors of THIS gameObject.
            // Notice, we don't support any arguments.
            gameObject.SendMessage(e.functionName, SendMessageOptions.RequireReceiver);
            _nextEvent_ix++;
        }//end while
    }
    public void Play()
    {
        if (clip == null) { return; }
        // Reset the animation time
        _animationTime = 0;
        _nextEvent_ix = 0;
        // Start animating
        _isAnimating = true;
    }
    public void Play(System.Action invokeThisOnce_onAnimComplete)
    {
        if (clip == null) { return; }
        _on_animComplete -= invokeThisOnce_onAnimComplete;
        _on_animComplete += invokeThisOnce_onAnimComplete;
        //aviods issues where update will only start running on the next frame:
        SetToFirstFrame();
        //launch:
        Play();
    }

    public void SetToFirstFrame()
    {
        if (clip == null) { return; }
        _animationTime = 0.0f;
        _nextEvent_ix = 0;
        clip.SampleAnimation(gameObject, 0);
        SampleEvents_ifNeeded();
    }


    // force_even_ifNot_playingClip= false  by default helps
    // to avoid "completing what actually was already completed on its own".
    // That's important because this function will invoke completion callbacks.
    public void ToLastFrame_thenComplete(bool force_even_ifNot_playingClip = false)
    {
        if (clip == null) { return; }
        if (force_even_ifNot_playingClip == false && _isAnimating == false) { return; }
        _animationTime = clip.length;

        clip.SampleAnimation(gameObject, clip.length);
        SampleEvents_ifNeeded();
        _isAnimating = false;
        //notice, called even if we are Looping (and have just looped back to start):
        if (_on_animComplete != null)
        {
            System.Action ptr = _on_animComplete;
            _on_animComplete = null;//NOTICE - set to null BEFORE invoking - during invoking the user might re-subscribe.
            ptr.Invoke();
        }
    }//end()
}