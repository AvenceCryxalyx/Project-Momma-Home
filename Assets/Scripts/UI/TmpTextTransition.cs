using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TmpTextTransition : MonoBehaviour
{
    [SerializeField]
    TMP_Text[] textMeshes;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float delayPerCharacter;
    [SerializeField]
    private float lingerDuration;

    private Mesh mesh;
    private Vector3[] textVertices;
    private float totalGroupDuration;
    [HideInInspector]
    public Vector3[] originalPositions;

    void Start()
    {
        originalPositions = new Vector3[textMeshes.Length];
        for (int i = 0; i < textMeshes.Length; i++)
        {
            originalPositions[i] = 1f * textMeshes[i].transform.position;
            totalGroupDuration += duration + (delayPerCharacter * textMeshes[i].text.Length);
        }
        HideAll();
    }

    public void PlaySequencedEnterFromCenterBottom(int index)
    {
        if (textMeshes[index] == null)
            return;
        textMeshes[index].transform.position = originalPositions[index];
        playTransition(textMeshes[index], enterFromCenterBottom(textMeshes[index], sequenceDuration(index) / totalGroupDuration, lingerDuration));
    }

    public void PlaySequencedPopEmUp(int index)
    {
        if (textMeshes[index] == null)
            return;
        textMeshes[index].transform.position = originalPositions[index];
        playTransition(textMeshes[index], popUp(textMeshes[index], sequenceDuration(index) / totalGroupDuration, lingerDuration));
    }

    public IEnumerator PopEmUp(int index, bool reverse, bool cleanup = true, string overrideText = "", bool skippable = true)
    {
        if (textMeshes[index] == null)
            yield break;
        if (!string.IsNullOrEmpty(overrideText))
            textMeshes[index].text = overrideText;
        textMeshes[index].transform.position = originalPositions[index];
        yield return popUp(textMeshes[index], startDelay: 0, lingerDuration: lingerDuration, reverse, cleanup);
    }

    public void HideAll()
    {
        for (int i = 0; i < textMeshes.Length; i++)
        {
            cleanUp(textMeshes[i]);
        }
    }

    float sequenceDuration(int index)
    {
        float sequence = 0;
        for (int i = 0; i < index; i++)
        {
            sequence += duration + (delayPerCharacter * textMeshes[i].textInfo.characterCount);
        }
        return sequence;
    }

    void cleanUp(TMP_Text subject)
    {
        subject.transform.position += Vector3.up * (Screen.height * 2);
        subject.ForceMeshUpdate();
    }

    void playTransition(TMP_Text subject, IEnumerator transition)
    {
        subject.ForceMeshUpdate();
        StartCoroutine(transition);
    }

    void stringFix(TMP_Text subject)
    {
        //place an invisible character at the start of the string
        //I did this because for some odd reason, the 1st character of the text will have funky behavior when the text has a space in it
        subject.text = $"{'\u2060'}{subject.text}";
        subject.ForceMeshUpdate();
        mesh = subject.mesh;
        textVertices = mesh.vertices;
    }

    Vector3[] prep(TMP_Text subject)
    {
        stringFix(subject);
        Vector3[] originalPositions = new Vector3[textVertices.Length];
        for (int i = 0; i < textVertices.Length; i++)
            originalPositions[i] = textVertices[i];
        return originalPositions;
    }

    void lerpVertices(TMP_Text subject, Vector3[] destinations, ref float deltaTime)
    {
        mesh = subject.mesh;
        textVertices = mesh.vertices;
        float delta = Time.deltaTime;
        deltaTime += delta;
        float time = deltaTime / duration;
        for (int i = 0; i < subject.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = subject.textInfo.characterInfo[i];
            int vertexIndex = charInfo.vertexIndex;
            float delayModifier = i * delayPerCharacter;
            for (int j = 0; j < 4; j++)
            {
                textVertices[vertexIndex + j] = Vector3.Lerp(textVertices[vertexIndex + j], destinations[vertexIndex + j], time - (delayModifier));
            }
        }
        mesh.vertices = textVertices;
        subject.canvasRenderer.SetMesh(mesh);
    }

    IEnumerator enterFromCenterBottom(TMP_Text subject, float startDelay, float lingerDuration, bool autoExit = true)
    {
        string originalText = subject.text;
        Vector3[] originalPositions = prep(subject);
        float scaleFactor = 15f;
        Vector3 textOffset = new Vector3(0f, subject.rectTransform.position.y + ((subject.rectTransform.rect.height * scaleFactor)), 0f);

        //=========Position to the bottom of the screen, then scale
        for (int i = 0; i < subject.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = subject.textInfo.characterInfo[i];
            int vertexIndex = charInfo.vertexIndex;
            if (i > 0)
            {
                for (int j = 0; j < 4; j++)
                {
                    Matrix4x4 matrix = Matrix4x4.TRS(textVertices[vertexIndex + j] - textOffset, Quaternion.identity, Vector3.one * scaleFactor);
                    textVertices[vertexIndex + j] = matrix.MultiplyPoint3x4(textVertices[vertexIndex + j]);
                }
                float characterWidth = (textVertices[vertexIndex + 2].x - textVertices[vertexIndex + 1].x) / 2f;
                for (int j = 0; j < 2; j++)
                {
                    textVertices[vertexIndex + j] -= new Vector3(textVertices[vertexIndex + j].x + characterWidth, 0, 0);
                }
                for (int j = 2; j < 4; j++)
                {
                    textVertices[vertexIndex + j] -= new Vector3(textVertices[vertexIndex + j].x - characterWidth, 0, 0);
                }
            }
        }
        mesh.vertices = textVertices;
        subject.canvasRenderer.SetMesh(mesh);
        //============
        yield return new WaitForSeconds(startDelay);
        //============Actual update function (lerp back to original state)
        float deltaTime = 0;
        float totalDuration = duration + (delayPerCharacter * subject.textInfo.characterCount);
        while (deltaTime < totalDuration)
        {
            lerpVertices(subject, originalPositions, ref deltaTime);
            yield return null;
        }
        //============
        yield return new WaitForSeconds(lingerDuration);
        if (autoExit)
        {
            yield return popUp(subject, 0, 0, reverse: true, cleanup: false, autoExit: false);
        }

        cleanUp(subject);
    }

    IEnumerator popUp(TMP_Text subject, float startDelay, float lingerDuration, bool reverse = false, bool cleanup = true, bool autoExit = true)
    {
        string originalText = subject.text;
        Vector3[] originalPositions = prep(subject);
        Vector3[] destinations = null;
        for (int i = 0; i < subject.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = subject.textInfo.characterInfo[i];
            int vertexIndex = charInfo.vertexIndex;
            if (i > 0)
            {
                textVertices[vertexIndex + 1] = textVertices[vertexIndex];
                textVertices[vertexIndex + 2] = textVertices[vertexIndex + 3];
            }
        }

        if (reverse)
        {
            destinations = new Vector3[textVertices.Length];
            for (int i = 0; i < textVertices.Length; i++) destinations[i] = textVertices[i];
        }

        destinations = reverse ? textVertices : originalPositions;
        mesh.vertices = reverse ? originalPositions : textVertices;
        subject.canvasRenderer.SetMesh(mesh);

        yield return new WaitForSeconds(startDelay);
        float totalDuration = duration + (delayPerCharacter * subject.textInfo.characterCount);
        float deltaTime = 0;

        while (deltaTime < totalDuration)
        {
            lerpVertices(subject, destinations, ref deltaTime);
            yield return null;
        }

        subject.text = originalText;
        if (!reverse)
        {
            yield return new WaitForSeconds(lingerDuration);
        }
        if (autoExit)
        {
            yield return popUp(subject, 0, 0, !reverse, cleanup: cleanup, autoExit: false);
            yield break;
        }

        if (cleanup)
        {
            cleanUp(subject);
        }
    }

    Vector2 wobbleTest(float time) => new Vector2(Mathf.Sin(time * 3.1f), Mathf.Cos(time * 3.1f));
}
