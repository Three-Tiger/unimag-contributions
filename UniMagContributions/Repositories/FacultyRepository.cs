using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;

namespace UniMagContributions.Repositories
{
	public class FacultyRepository : IFacultyRepository
	{
		private readonly ApplicationDbContext _context;

		public FacultyRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public void CreateFaculty(Faculty faculty)
		{
			try
			{
				_context.Faculties.Add(faculty);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error creating faculty");
			}
		}

		public void DeleteFaculty(Faculty faculty)
		{
			try
			{
				_context.Faculties.Remove(faculty);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error deleting faculty");
			}
		}

		public List<Faculty> GetAllFaculty()
		{
			try
			{
				return _context.Faculties.Where(f => !f.Name.Equals("Admin")).ToList();
			}
			catch (Exception)
			{
				throw new Exception("Error fetch faculty");
			}
		}

		public Faculty GetFacultyById(Guid id)
		{
			try
			{
				return _context.Faculties.Where(u => u.FacultyId == id).AsNoTracking().FirstOrDefault();
			}
			catch (Exception)
			{
				throw new Exception("Error getting faculty");
			}
		}

		public Faculty GetFacultyByName(string name)
		{
			try
			{
				Faculty faculty = _context.Faculties.FirstOrDefault(u => u.Name == name);
				return faculty;
			}
			catch (Exception)
			{
				throw new Exception("Error getting faculty");
			}
		}

		public void UpdateFaculty(Faculty faculty)
		{
			try
			{
				_context.Entry<Faculty>(faculty).State = EntityState.Modified;
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error updating faculty");
			}
		}
	}
}
