using Godot;

public partial class UI : ISave
{
	private ConfigFile _config;
	private readonly string _pathSave = "user://save.ini";

	public void Load()
	{
		_config = new ConfigFile();
		var err = _config.Load(_pathSave);
		if(err != Error.Ok)
			return;
		_bestScore = (int)_config.GetValue("Score", "BestScore");
	}

	public void Save()
	{
		_config.SetValue("Score", "BestScore", _bestScore);
		_config.Save(_pathSave);
	}
}
