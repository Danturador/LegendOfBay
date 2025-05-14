using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using _ProjectFiles.SaveSystem;

public class FogOfWarController : MonoBehaviour
{
	[Inject] private SaveSystemController saveSystemController;
	[Inject] private InputController inputController;
	public Texture2D fogOfWarTexture;
	public Texture2D texture;
	public SpriteMask spriteMask;
	[SerializeField] private SpriteRenderer fog;

	private Vector2 worldScale;
	private Vector2Int pixelScale;

	public void Awake()
	{
		fog.gameObject.SetActive(true);
		LoadTexture();
		InitializeWorldScale();

		inputController.Gameplay.OpenMap.performed += ctx => CreateSprite();
	}

	private void LoadTexture()
	{
		byte[] tex = saveSystemController.gameData.MapTexture;
		if (tex != null)
		{
			fogOfWarTexture = new Texture2D(1024, 1024);
			fogOfWarTexture.LoadImage(tex);
		}
	}

	private void InitializeWorldScale()
	{
		pixelScale.x = fogOfWarTexture.width;
		pixelScale.y = fogOfWarTexture.height;
		worldScale.x = pixelScale.x / 100f * transform.localScale.x;
		worldScale.y = pixelScale.y / 100f * transform.localScale.y;
	}

	private Vector2Int WorldToPixel(Vector2 position)
	{
		Vector2Int pixelPosition = Vector2Int.zero;
		float dx = position.x - transform.position.x;
		float dy = position.y - transform.position.y;

		pixelPosition.x = Mathf.RoundToInt(0.5f * pixelScale.x + dx * (pixelScale.x / worldScale.x));
		pixelPosition.y = Mathf.RoundToInt(0.5f * pixelScale.y + dy * (pixelScale.y / worldScale.y));

		return pixelPosition;
	}

	//public void MakeHole(Vector2 position, float holeRadius)
	public async Task MakeHole(Vector2 position, float holeRadius)
	{
		Vector2Int pixelPosition = WorldToPixel(position);
		int radius = Mathf.RoundToInt(holeRadius * pixelScale.x / worldScale.x);

		await Task.Run(() =>
		{
			for (int i = 0; i < radius; i++)
			{
				int distance = Mathf.RoundToInt(Mathf.Sqrt(radius * radius - i * i));

				for (int j = 0; j < distance; j++)
				{
					int px = Mathf.Clamp(pixelPosition.x + i, 0, pixelScale.x - 1);
					int nx = Mathf.Clamp(pixelPosition.x - i, 0, pixelScale.x - 1);
					int py = Mathf.Clamp(pixelPosition.y + j, 0, pixelScale.y - 1);
					int ny = Mathf.Clamp(pixelPosition.y - j, 0, pixelScale.y - 1);

					lock (fogOfWarTexture)
					{
						fogOfWarTexture.SetPixel(px, py, Color.black);
						fogOfWarTexture.SetPixel(nx, py, Color.black);
						fogOfWarTexture.SetPixel(px, ny, Color.black);
						fogOfWarTexture.SetPixel(nx, ny, Color.black);
					}
				}
			}

			ApplyTexture();
		});
	}

	private void ApplyTexture()
	{
		fogOfWarTexture.Apply();
	}
	private void CreateSprite()
	{
		spriteMask.sprite = Sprite.Create(fogOfWarTexture, new Rect(0, 0, fogOfWarTexture.width, fogOfWarTexture.height), Vector2.one * 0.5f, 100);
	}

	public void SaveFoWProgress()
	{
		saveSystemController.UpdateTexture(fogOfWarTexture);
		saveSystemController.SaveProgress();
	}
}