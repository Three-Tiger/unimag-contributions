using AutoMapper;
using NuGet.Protocol.Plugins;
using UniMagContributions.Dto.Faculty;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
	public class FacultyService : IFacultyService
	{
		private readonly IMapper _mapper;
		private readonly IFacultyRepository _facultyRepository;

		public FacultyService(IMapper mapper, IFacultyRepository repository)
		{
			_mapper = mapper;
			_facultyRepository = repository;
		}

		public FacultyDto AddFaculty(CreateFacultyDto createFacultyDto)
		{
			Faculty faculty = _facultyRepository.GetFacultyByName(createFacultyDto.Name);

			if (faculty != null)
			{
				throw new ConflictException("Faculty already exists");
			}

			faculty = _mapper.Map<Faculty>(createFacultyDto);
			_facultyRepository.CreateFaculty(faculty);

			return _mapper.Map<FacultyDto>(faculty);
		}

		public string DeleteFaculty(Guid id)
		{
			Faculty faculty = _facultyRepository.GetFacultyById(id)??throw new NotFoundException("Faculty does not exists");
			_facultyRepository.DeleteFaculty(faculty);

			return "Delete successful";
		}

		public List<FacultyDto> GetAllFaculty()
		{
			List<Faculty> facultyList = _facultyRepository.GetAllFaculty();
			return _mapper.Map<List<FacultyDto>>(facultyList);
		}

		public FacultyDto GetFacultyById(Guid id)
		{
			Faculty faculty = _facultyRepository.GetFacultyById(id)??throw new NotFoundException("Faculty does not exists"); ;

			return _mapper.Map<FacultyDto>(faculty);
		}

		public FacultyDto UpdateFaculty(Guid id, UpdateFacultyDto updateFacultyDto)
		{
			_ = _facultyRepository.GetFacultyById(id)??throw new NotFoundException("Faculty does not exists");

			updateFacultyDto.FacultyId = id;

			Faculty facultyToUpdate = _mapper.Map<Faculty>(updateFacultyDto);
			_facultyRepository.UpdateFaculty(facultyToUpdate);

			return _mapper.Map<FacultyDto>(facultyToUpdate);
		}
	}
}
