using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;

namespace UniMagContributions.Repositories
{
	public class AnnualMagazineRepository : IAnnualMagazineRepository
	{
		private readonly ApplicationDbContext _context;

		public AnnualMagazineRepository(ApplicationDbContext context)
		{
			_context = context;
		}


		public void CreateAnnualMagazine(AnnualMagazine annualMagazine)
		{
			try
			{
				_context.AnnualMagazines.Add(annualMagazine);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error creating Annual Magazine");
			}
		}

		public void DeleteAnnualMagazine(AnnualMagazine annualMagazine)
		{
			try
			{
				_context.AnnualMagazines.Remove(annualMagazine);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error deleting Annual Magazine");
			}
		}

		public List<AnnualMagazine> GetAllAnnualMagazine()
		{
			try
			{
				return _context.AnnualMagazines.OrderByDescending(a => a.FinalClosureDate).ToList();
			}
			catch (Exception)
			{
				throw new Exception("Error fetch Magazine");
			}
		}

		public AnnualMagazine GetAnnualMagazineById(Guid id)
		{
			try
			{
				return _context.AnnualMagazines.Where(u => u.AnnualMagazineId == id).AsNoTracking().FirstOrDefault();
			}
			catch (Exception)
			{
				throw new Exception("Error getting Annual Magazine");
			}
		}

		public AnnualMagazine GetAnnualMagazineByAcademicYear(string year)
		{
			try
			{
				AnnualMagazine annualMagazine = _context.AnnualMagazines.FirstOrDefault(u => u.AcademicYear == year);
				return annualMagazine;
			}
			catch (Exception)
			{
				throw new Exception("Error getting Annual Magazine");
			}
		}

		public void UpdateAnnualMagazine(AnnualMagazine annualMagazine)
		{
			try
			{
				_context.Entry<AnnualMagazine>(annualMagazine).State = EntityState.Modified;
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error updating Annual Magazine");
			}
		}

		public bool CheckUpdateAcademicYear(string academicYear, Guid id)
		{
            try
			{
                AnnualMagazine annualMagazine = _context.AnnualMagazines.AsNoTracking().FirstOrDefault(u => u.AcademicYear == academicYear);
                if (annualMagazine != null)
				{
                    if (annualMagazine.AnnualMagazineId != id)
					{
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
			{
                throw new Exception("Error checking Academic Year");
            }
        }
	}
}
