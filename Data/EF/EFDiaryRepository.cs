using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public class EFDiaryRepository : IDiaryRepository {
    private BeonDbContext context;
    public EFDiaryRepository(BeonDbContext ctx) {
      context = ctx;
    }

    public void SaveDiary(Diary Diary) {
      context.Diaries.Add(Diary);
      context.SaveChanges();
    }
    public IQueryable<Diary> Diaries => context.Diaries;
  }
}