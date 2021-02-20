using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlink : SkillBase
{
    public GameObject caster;

    public override void StartAction()
    {
        base.StartAction();

        SkillInstancedata data = GetComponent<SkillInstancedata>();

        caster = data.caster;

        caster.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);
    }

    public override void StartVisuals()
    {
        base.StartVisuals();
    }


    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject);
    }


}
