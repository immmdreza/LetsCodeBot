using LetsCodeApp.Models.BaseModels;
using LetsCodeApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LetsCodeApp.Services
{
    class GroupServices : Repository<Group>
    {
        public async Task<bool> CheckByTlId(long telegramId)
        {
            return await _applicationContext.Groups.AnyAsync(x => x.TelegramId == telegramId);
        }

        public async Task<Group> GetByTlId(long telegramId)
        {
            return await _applicationContext.Groups.FirstOrDefaultAsync(x => x.TelegramId == telegramId);
        }

        public async Task<string> WlcmsgByTlId(long telegramId)
        {
            return await _applicationContext.Groups
                .Where(x => x.TelegramId == telegramId)
                .Select(x => x.WelcomeMessage)
                .SingleOrDefaultAsync();
        }

        public async Task UpdateWelcome(long telegramId, string st)
        {
            var gp = await _applicationContext.Groups
                .Where(x => x.TelegramId == telegramId)
                .SingleOrDefaultAsync();

            gp.WelcomeMessage = st;

            await _applicationContext.SaveChangesAsync();
        }

        public async Task UpdateAbout(long telegramId, string st)
        {
            var gp = await _applicationContext.Groups
                .Where(x => x.TelegramId == telegramId)
                .SingleOrDefaultAsync();

            gp.About = st;

            await _applicationContext.SaveChangesAsync();
        }
    }
}
