using Grpc.Core;
using Project.Common.Enum;
using Project.Core.AWS;
using Project.ProfileService.Data;
using Project.ProfileService.Data.Configurations;
using Project.ProfileService.Protos;
using Project.ProfileService.Repository.HealthProfileRepository;
using Project.ProfileService.Repository.ProfileRepository;

namespace Project.ProfileService.Service
{
    public class ProfileDataService : Protos.ProfileService.ProfileServiceBase
    {

        private readonly IProfileRepository profileRepository;
        private readonly IHealthProfileRepository healthProfileRepository;
        private readonly IAmazonS3Bucket s3Bucket;

        public ProfileDataService(IProfileRepository profileRepository, IHealthProfileRepository healthProfileRepository, IAmazonS3Bucket s3Bucket)
        {
            this.profileRepository = profileRepository;
            this.healthProfileRepository = healthProfileRepository;
            this.s3Bucket = s3Bucket;
        }

        public override async Task<EmailExistResponse> EmailIsExist(CheckEmailRequest request, ServerCallContext context)
        {
            try
            {
                var result = await profileRepository.GetAsync(x => x.Email == request.Email);
                var res = new EmailExistResponse();
                if (result != null)
                {
                    res.IsExist = true;
                    res.UserID = result.UserID.ToString();
                    return res;
                }
                res.IsExist = false;
                return res;
            }
            catch
            {
                var res = new EmailExistResponse();
                return res;
            }
        }
        public override async Task<CreateProfileResponse> CreateProfile(CreateProfileRequest request, ServerCallContext context)
        {
            try
            {
                var profileExist = await profileRepository.AnyAsync(x => x.Email == request.Email);
                if (profileExist)
                {
                    var x = new CreateProfileResponse();
                    x.IsSuccess = false;
                    return x;
                }
                var profile = new Data.Profile
                {
                    UserID = Guid.Parse(request.UserID),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth.ToDateTime(),
                    Gender = request.Gender,
                    Email = request.Email
                };
                profile.Avatar = ConstantsData.DefaultAvatarURL;
                var result = await profileRepository.CreateEntityAsync(profile);
                var res = new CreateProfileResponse();
                if (result == null)
                {
                    res.IsSuccess = false;
                    return res;
                }
                var health = new HealthProfile { ProfileID = result.ProfileID, RelationshipID = ConstantsData.MyRelationshipID, BloodType = "A+" };
                var healthResult = await healthProfileRepository.CreateAsync(health);
                if (!healthResult)
                {
                    await profileRepository.DeleteAsync(profile);
                    res.IsSuccess = false;
                    return res;
                }
                res.IsSuccess = true;
                return res;
            }
            catch
            {
                var res = new CreateProfileResponse();
                res.IsSuccess = false;
                return res;
            }
        }

        public override async Task<GetProfileResponse> GetProfile(GetProfileRequest request, ServerCallContext context)
        {
            try
            {
                var profiles = await profileRepository.GetProfilesAsync(Guid.Parse(request.UserID));
                var res = new GetProfileResponse();
                if (profiles == null)
                {
                    return res;
                }
                if (profiles.Count > 1)
                {
                    foreach (var profile in profiles)
                    {
                        if (profile.HealthProfile.RelationshipID == ConstantsData.MyRelationshipID)
                        {
                            res.UserID = profile.UserID.ToString(); ;
                            res.Avatar = profile.Avatar;
                            res.FirstName = profile.FirstName;
                            res.LastName = profile.LastName;
                            res.Email = profile.Email;
                            return res;
                        }
                    }
                }
                res.UserID = profiles[0].UserID.ToString(); ;
                res.Avatar = profiles[0].Avatar;
                res.FirstName = profiles[0].FirstName;
                res.LastName = profiles[0].LastName;
                res.Email = profiles[0].Email;
                return res;
            }
            catch
            {
                var res = new GetProfileResponse();
                return res;
            }
        }
        public override async Task<GetAllProfileResponse> GetAllProfile(GetAllProfileRequest request, ServerCallContext context)
        {
            try
            {
                var UserIDs = request.UserIDs.Select(Guid.Parse).ToList();
                var profiles = await profileRepository.GetManyProfilesAsync(UserIDs);
                var res = new GetAllProfileResponse();
                if (profiles == null)
                {
                    return res;
                }
                profiles.RemoveAll(profile =>
                {
                    if (profile.HealthProfile == null)
                    {
                        return false;
                    }
                    if (profile.HealthProfile.RelationshipID != ConstantsData.MyRelationshipID)
                    {
                        return true;
                    }
                    return false;
                });
                GetAllProfileResponse getAllProfileResponse = new GetAllProfileResponse();
                List<GetAllProfile> allProfiles = new List<GetAllProfile>();
                foreach (var id in UserIDs)
                {
                    Profile profile = null;
                    if (id == Guid.Empty)
                    {
                        profile = new Profile
                        {
                            UserID = id,
                            Avatar = ConstantsData.DefaultAvatarURL,
                            FirstName = "Supporter",
                            LastName = ""
                        };
                    }
                    else
                    {
                        profile = profiles.SingleOrDefault(x => x.UserID == id);
                    }

                    allProfiles.Add(new GetAllProfile
                    {
                        UserID = profile.UserID.ToString(),
                        Avatar = profile.Avatar,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName
                    });
                }

                getAllProfileResponse.Profiles.AddRange(allProfiles);
                return getAllProfileResponse;
            }
            catch
            {
                var res = new GetAllProfileResponse();
                return res;
            }
        }

        public override async Task<GetAllUserProfileResponse> GetAllUserProfile(GetAllUserProfileRequest request, ServerCallContext context)
        {
            try
            {
                var ProfileIDs = request.ProfileIDs.Select(Guid.Parse).ToList();
                var profiles = await profileRepository.GetAllAsync(x => ProfileIDs.Contains(x.ProfileID));
                var res = new GetAllUserProfileResponse();
                if (profiles == null)
                {
                    return res;
                }
                GetAllUserProfileResponse getAllUserProfileResponse = new GetAllUserProfileResponse();
                List<GetUserProfileResponse> allProfiles = new List<GetUserProfileResponse>();
                foreach (var id in ProfileIDs)
                {
                    var profile = profiles.SingleOrDefault(x => x.ProfileID == id);
                    allProfiles.Add(new GetUserProfileResponse
                    {
                        ProfileID = profile.ProfileID.ToString(),
                        UserID = profile.UserID.ToString(),
                        Avatar = profile.Avatar,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName
                    });
                }

                getAllUserProfileResponse.Profile.AddRange(allProfiles);
                return getAllUserProfileResponse;
            }
            catch
            {
                var res = new GetAllUserProfileResponse();
                return res;
            }
        }

        public override async Task<GetDoctorAndUserProfileResponse> GetDoctorAndUserProfile(GetDoctorAndUserProfileRequest request, ServerCallContext context)
        {
            try
            {
                var UserProfileIDs = request.UserProfileIDs.Select(Guid.Parse).ToList();
                var DoctorProfileIDs = request.DoctorProfileIDs.Select(Guid.Parse).ToList();
                var UserProfiles = await profileRepository.GetAllAsync(x => UserProfileIDs.Contains(x.ProfileID));
                var DoctorProfiles = await profileRepository.GetAllAsync(x => DoctorProfileIDs.Contains(x.UserID));
                var res = new GetDoctorAndUserProfileResponse();
                if (UserProfiles == null)
                {
                    return null;
                }
                if (DoctorProfiles == null)
                {
                    return null;
                }
                GetDoctorAndUserProfileResponse DoctorAndUserProfile = new GetDoctorAndUserProfileResponse();
                List<DoctorAndUserProfileResponse> Profiles = new List<DoctorAndUserProfileResponse>();
                for (int i = 0; i < UserProfileIDs.Count; i++)
                {
                    var DoctorProfile = DoctorProfiles.SingleOrDefault(x => x.UserID == DoctorProfileIDs[i]);
                    var UserProfile = UserProfiles.SingleOrDefault(x => x.ProfileID == UserProfileIDs[i]);
                    Profiles.Add(new DoctorAndUserProfileResponse
                    {
                        DoctorProfileID = DoctorProfile.ProfileID.ToString(),
                        DoctorUserID = DoctorProfile.UserID.ToString(),
                        DoctorFirstName = DoctorProfile.FirstName,
                        DoctorLastName = DoctorProfile.LastName,
                        DoctorAvatar = DoctorProfile.Avatar,
                        UserProfileID = UserProfile.ProfileID.ToString(),
                        UserUserID = UserProfile.UserID.ToString(),
                        UserFirstName = UserProfile.FirstName,
                        UserLastName = UserProfile.LastName,
                        UserAvatar = UserProfile.Avatar,
                    });
                }

                DoctorAndUserProfile.Profile.AddRange(Profiles);
                return DoctorAndUserProfile;
            }
            catch
            {
                return null;
            }
        }

        public override async Task<GetPatientProfileResponse> GetPatientProfile(GetPatientProfileRequest request, ServerCallContext context)
        {
            try
            {
                var ProfileID = Guid.Parse(request.ProfileID);
                var profile = await profileRepository.GetProfileByIDAsync(ProfileID);
                if (profile == null)
                {
                    return null;
                }
                GetPatientProfileResponse response = new GetPatientProfileResponse
                {
                    ProfileID = profile.ProfileID.ToString(),
                    UserID = profile.UserID.ToString(),
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Avatar = profile.Avatar,
                    Address = profile.Address,
                    DateOfBirth = profile.DateOfBirth.ToString(),
                    Email = profile.Email,
                    Gender = profile.Gender,
                    BloodType = profile.HealthProfile.BloodType,
                    Phone = profile.Phone,
                    Height = profile.HealthProfile.Height,
                    Weight = profile.HealthProfile.Weight,
                    Relationship = profile.HealthProfile.Relationship.RelationshipName
                };
                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}
