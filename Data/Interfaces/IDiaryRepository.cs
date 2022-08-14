namespace Beon.Models {
  public interface IDiaryRepository {
    IQueryable<Diary> Diaries { get; }

    void SaveDiary(Diary Diary);
  }
}