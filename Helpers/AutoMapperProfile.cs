using AutoMapper;
using Dksh.ePOD.Entities;
using Dksh.ePOD.Models;

namespace Dksh.ePOD.Helpers
{
    /// <summary>
    /// A class that setup the automapper mapping configuration.
    /// More detail can be obtained from https://code-maze.com/automapper-net-core/
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ExternalQuestionnaireBO, QFormModelVN>();
            CreateMap<ExternalQuestionnaireBO, QFormModelMY>()
                .AfterMap((src, dst) =>
                {
                    if (src.OtherCountry != null)
                    {
                        if (src.OtherCountry.GetType() == typeof(ExternalQuestionnaireMYBO))
                        {
                            ExternalQuestionnaireMYBO b = (ExternalQuestionnaireMYBO)src.OtherCountry;
                            dst.often_training = b.Often_training;
                            dst.anti_bribe_policy = b.anti_bribe_policy;
                            dst.compliance_and_ethics_7 = b.compliance_and_ethics_7;
                            dst.compliance_and_ethics_8 = b.compliance_and_ethics_8;
                        }
                    }
            });

            CreateMap<QFormModelVN, ExternalQuestionnaireBO>();
            CreateMap<QFormModelMY, ExternalQuestionnaireBO>();
        }
    }
}
