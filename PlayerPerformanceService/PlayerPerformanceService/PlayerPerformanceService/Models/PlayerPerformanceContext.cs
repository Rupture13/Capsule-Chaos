using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace PlayerPerformanceService.Models
{
    public class PlayerPerformanceContext : DbContext
    {
        public PlayerPerformanceContext(DbContextOptions<PlayerPerformanceContext> options)
            : base(options)
        {
            //TODO: Remove this method call and the method itself after testing
            SeedContextOnStartup();
        }

        public virtual DbSet<PlayerPerformance> PlayerPerformances { get; set; }
        public virtual DbSet<PerformanceSnapshot> PerformanceSnapshots { get; set; }

        private void SeedContextOnStartup()
        {
            if (this.PlayerPerformances.Find(1) != null)
            {
                return;
            }

            var snapshot1 = new PerformanceSnapshot
            {
                Timestamp = 0.5f,
                Location = new Vector3Ser(0f, 1.996075987815857f, 0f),
                Rotation = new QuaternionSer(0f, 0f, 0f, 1.0f)
            };
            var snapshot2 = new PerformanceSnapshot
            {
                Timestamp = 1.0f,
                Location = new Vector3Ser(-0.007914123125374318f, 0.7438715696334839f, 0.14086785912513734f),
                Rotation = new QuaternionSer(0f, 0f, 0f, 1.0f)
            };
            var snapshot3 = new PerformanceSnapshot
            {
                Timestamp = 1.5f,
                Location = new Vector3Ser(-0.05057601258158684f, 0.5432894825935364f, 1.0941189527511597f),
                Rotation = new QuaternionSer(0.6428659558296204f, -0.00018632641877047718f, 0.028705431148409845f, 0.7654407024383545f)
            };
            var snapshot4 = new PerformanceSnapshot
            {
                Timestamp = 2.0f,
                Location = new Vector3Ser(-0.12303921580314636f, 0.6787381768226624f, 2.8103134632110597f),
                Rotation = new QuaternionSer(0.9234148263931274f, -0.001861668424680829f, 0.039812639355659488f, -0.38172850012779238f)
            };
            var snapshot5 = new PerformanceSnapshot
            {
                Timestamp = 2.5f,
                Location = new Vector3Ser(0.05084504932165146f, 0.697434663772583f, 4.866882801055908f),
                Rotation = new QuaternionSer(-0.3198240101337433f, -0.0493151992559433f, 0.07855615764856339f, -0.9429261088371277f)
            };
            var snapshot6 = new PerformanceSnapshot
            {
                Timestamp = 3.0f,
                Location = new Vector3Ser(1.260588526725769f, 0.7084556221961975f, 6.535645484924316f),
                Rotation = new QuaternionSer(-0.8648927211761475f, 0.2255755066871643f, 0.39159733057022097f, 0.21846701204776765f)
            };
            var snapshot7 = new PerformanceSnapshot
            {
                Timestamp = 3.5f,
                Location = new Vector3Ser(3.312086582183838f, 0.7598157525062561f, 7.724052906036377f),
                Rotation = new QuaternionSer(0.2953311800956726f, 0.44268494844436648f, -0.163438081741333f, 0.8307211399078369f)
            };
            var snapshot8 = new PerformanceSnapshot
            {
                Timestamp = 4.0f,
                Location = new Vector3Ser(5.7139973640441898f, 0.7276809215545654f, 8.562849044799805f),
                Rotation = new QuaternionSer(0.6819512844085693f, -0.35008254647254946f, -0.5658206343650818f, -0.30369681119918826f)
            };
            var snapshot9 = new PerformanceSnapshot
            {
                Timestamp = 4.5f,
                Location = new Vector3Ser(7.123627185821533f,
                  0.6604511737823486f,
                  9.558085441589356f),
                Rotation = new QuaternionSer(-0.3620637357234955f,
                  -0.41333961486816409f,
                  -0.22331833839416505f,
                  -0.8051019906997681f)
            };
            var snapshot10 = new PerformanceSnapshot
            {
                Timestamp = 5.0f,
                Location = new Vector3Ser(7.681317329406738f,
                  0.7406455874443054f,
                  10.76095199584961f),
                Rotation = new QuaternionSer(-0.9872421622276306f,
                  0.021687515079975129f,
                  -0.07263835519552231f,
                  -0.14002257585525514f)
            };
            var snapshot11 = new PerformanceSnapshot
            {
                Timestamp = 5.5f,
                Location = new Vector3Ser(7.433403491973877f,
                  0.578610897064209f,
                  12.185981750488282f),
                Rotation = new QuaternionSer(-0.5798734426498413f,
                  -0.13515089452266694f,
                  0.08707670867443085f,
                  0.7986856698989868f)
            };
            var snapshot12 = new PerformanceSnapshot
            {
                Timestamp = 6.0f,
                Location = new Vector3Ser(6.30675745010376f,
                  0.5349901914596558f,
                  13.679370880126954f),
                Rotation = new QuaternionSer(0.6059023141860962f,
                  -0.3407554030418396f,
                  0.4503704607486725f,
                  0.5602988004684448f)
            };
            var snapshot13 = new PerformanceSnapshot
            {
                Timestamp = 6.5f,
                Location = new Vector3Ser(4.708486080169678f,
                  0.5530208349227905f,
                  15.23013687133789f),
                Rotation = new QuaternionSer(0.6251615285873413f,
                  0.1880246102809906f,
                  0.09863178431987763f,
                  -0.7510603070259094f)
            };
            var snapshot14 = new PerformanceSnapshot
            {
                Timestamp = 7.0f,
                Location = new Vector3Ser(2.8760457038879396f,
                  0.5989409685134888f,
                  16.559497833251954f),
                Rotation = new QuaternionSer(-0.6495572924613953f,
                  0.38006624579429629f,
                  -0.5241296887397766f,
                  -0.39863914251327517f)
            };
            var snapshot15 = new PerformanceSnapshot
            {
                Timestamp = 7.5f,
                Location = new Vector3Ser(
                  0.5881730318069458f,
                  0.6236072778701782f,
                  17.39064598083496f),
                Rotation = new QuaternionSer(
                  -0.5119190812110901f,
                  -0.3863008916378021f,
                  -0.017434734851121904f,
                  0.7670766115188599f)
            };
            var snapshot16 = new PerformanceSnapshot
            {
                Timestamp = 8.0f,
                Location = new Vector3Ser(
                  -2.0844147205352785f,
                  0.6758556962013245f,
                  17.613418579101564f),
                Rotation = new QuaternionSer(
                  0.5954486131668091f,
                  -0.3856094181537628f,
                  0.7021976113319397f,
                  -0.060538873076438907f)
            };
            var snapshot17 = new PerformanceSnapshot
            {
                Timestamp = 8.5f,
                Location = new Vector3Ser(
                  -4.329534530639648f,
                  0.6916583180427551f,
                  17.763051986694337f),
                Rotation = new QuaternionSer(
                  0.3067615032196045f,
                  0.591062068939209f,
                  -0.15780547261238099f,
                  -0.7291368246078491f)
            };
            var snapshot18 = new PerformanceSnapshot
            {
                Timestamp = 9.0f,
                Location = new Vector3Ser(
                  -5.903555393218994f,
                  0.6055600643157959f,
                  17.9722900390625f),
                Rotation = new QuaternionSer(
                  -0.5814917087554932f,
                  0.537083625793457f,
                  -0.6099191308021545f,
                  -0.037512972950935367f)
            };
            var snapshot19 = new PerformanceSnapshot
            {
                Timestamp = 9.5f,
                Location = new Vector3Ser(
                  -7.375308513641357f,
                  0.5350220799446106f,
                  18.435985565185548f),
                Rotation = new QuaternionSer(
                  -0.6642306447029114f,
                  -0.1892232894897461f,
                  -0.07931577414274216f,
                  0.7188193202018738f)
            };
            var snapshot20 = new PerformanceSnapshot
            {
                Timestamp = 10.0f,
                Location = new Vector3Ser(
                  -9.579723358154297f,
                  0.5741841197013855f,
                  18.25315284729004f),
                Rotation = new QuaternionSer(
                  0.161786749958992f,
                  -0.6510853171348572f,
                  0.7002116441726685f,
                  0.24416516721248628f)
            };
            var snapshot21 = new PerformanceSnapshot
            {
                Timestamp = 10.5f,
                Location = new Vector3Ser(
                  -11.812639236450196f,
                  1.0828194618225098f,
                  17.32432746887207f),
                Rotation = new QuaternionSer(
                  0.8298008441925049f,
                  -0.05837244912981987f,
                  0.19226792454719544f,
                  -0.5206306576728821f)
            };
            var snapshot22 = new PerformanceSnapshot
            {
                Timestamp = 11.0f,
                Location = new Vector3Ser(
                  -11.771505355834961f,
                  2.5376811027526857f,
                  16.085838317871095f),
                Rotation = new QuaternionSer(
                  0.1283893883228302f,
                  0.3679145574569702f,
                  -0.9132816791534424f,
                  0.1186237558722496f)
            };
            var snapshot23 = new PerformanceSnapshot
            {
                Timestamp = 11.5f,
                Location = new Vector3Ser(
                  -11.79508113861084f,
                  4.368398189544678f,
                  15.229256629943848f),
                Rotation = new QuaternionSer(
                  -0.7495535016059876f,
                  -0.023396959528326989f,
                  0.20025674998760224f,
                  0.6304914951324463f)
            };
            var snapshot24 = new PerformanceSnapshot
            {
                Timestamp = 12.0f,
                Location = new Vector3Ser(
                  -11.764166831970215f,
                  6.271305561065674f,
                  15.219987869262696f),
                Rotation = new QuaternionSer(
                  0.08018004894256592f,
                  -0.6911330819129944f,
                  0.6190346479415894f,
                  -0.36428332328796389f)
            };
            var snapshot25 = new PerformanceSnapshot
            {
                Timestamp = 12.5f,
                Location = new Vector3Ser(
                  -11.815001487731934f,
                  7.972695350646973f,
                  15.840571403503418f),
                Rotation = new QuaternionSer(
                  0.5039714574813843f,
                  0.3782811760902405f,
                  -0.23879535496234895f,
                  -0.7388456463813782f)
            };
            var snapshot26 = new PerformanceSnapshot
            {
                Timestamp = 13.0f,
                Location = new Vector3Ser(
                  -11.748619079589844f,
                  8.865439414978028f,
                  17.067758560180665f),
                Rotation = new QuaternionSer(
                  0.1333129107952118f,
                  0.8980222940444946f,
                  -0.2388707846403122f,
                  0.3445641100406647f)
            };
            var snapshot27 = new PerformanceSnapshot
            {
                Timestamp = 13.5f,
                Location = new Vector3Ser(
                  -11.814671516418457f,
                  8.937128067016602f,
                  18.460472106933595f),
                Rotation = new QuaternionSer(
                  0.05862067639827728f,
                  -0.08455207198858261f,
                  -0.07537391036748886f,
                  0.9918333888053894f)
            };
            var snapshot28 = new PerformanceSnapshot
            {
                Timestamp = 14.0f,
                Location = new Vector3Ser(
                  -11.798563003540039f,
                  8.70505428314209f,
                  19.316635131835939f),
                Rotation = new QuaternionSer(
                  0.013854586519300938f,
                  -0.7784385681152344f,
                  -0.19016849994659425f,
                  0.5980613231658936f)
            };
            var snapshot29 = new PerformanceSnapshot
            {
                Timestamp = 14.5f,
                Location = new Vector3Ser(
                  -11.632211685180664f,
                  8.131375312805176f,
                  19.29191017150879f),
                Rotation = new QuaternionSer(
                  -0.4073666036128998f,
                  -0.7448742985725403f,
                  -0.30332064628601076f,
                  0.43267926573753359f)
            };
            var snapshot30 = new PerformanceSnapshot
            {
                Timestamp = 15.0f,
                Location = new Vector3Ser(
                  -11.758304595947266f,
                  6.961946487426758f,
                  18.097997665405275f),
                Rotation = new QuaternionSer(
                  -0.8839823007583618f,
                  0.19231542944908143f,
                  0.027874018996953965f,
                  0.4252212941646576f)
            };
            var snapshot31 = new PerformanceSnapshot
            {
                Timestamp = 15.5f,
                Location = new Vector3Ser(
                  -11.749521255493164f,
                  5.644134998321533f,
                  16.401901245117189f),
                Rotation = new QuaternionSer(
                  0.14409223198890687f,
                  0.869612455368042f,
                  0.44065481424331667f,
                  -0.16980856657028199f)
            };
            var snapshot32 = new PerformanceSnapshot
            {
                Timestamp = 16.0f,
                Location = new Vector3Ser(
                  -11.782419204711914f,
                  4.623794078826904f,
                  15.043045997619629f),
                Rotation = new QuaternionSer(
                  0.8873079419136047f,
                  0.1304263472557068f,
                  0.041476886719465259f,
                  -0.440401166677475f)
            };
            var snapshot33 = new PerformanceSnapshot
            {
                Timestamp = 16.5f,
                Location = new Vector3Ser(
                  -11.699594497680664f,
                  4.728460311889648f,
                  13.81548023223877f),
                Rotation = new QuaternionSer(
                  0.5911105871200562f,
                  -0.2465052306652069f,
                  -0.7391800880432129f,
                  -0.20841355621814729f)
            };
            var snapshot34 = new PerformanceSnapshot
            {
                Timestamp = 17.0f,
                Location = new Vector3Ser(
                  -11.601581573486329f,
                  6.006484508514404f,
                  13.380155563354493f),
                Rotation = new QuaternionSer(
                  0.28285685181617739f,
                  0.18158721923828126f,
                  -0.680652916431427f,
                  0.650945246219635f)
            };
            var snapshot35 = new PerformanceSnapshot
            {
                Timestamp = 17.5f,
                Location = new Vector3Ser(
                  -11.625040054321289f,
                  8.224908828735352f,
                  13.86573600769043f),
                Rotation = new QuaternionSer(
                  -0.08883342891931534f,
                  0.12564118206501008f,
                  0.4639609456062317f,
                  0.8723893761634827f)
            };
            var snapshot36 = new PerformanceSnapshot
            {
                Timestamp = 18.0f,
                Location = new Vector3Ser(
                  -11.606898307800293f,
                  10.66489028930664f,
                  15.837359428405762f),
                Rotation = new QuaternionSer(
                  -0.2635340094566345f,
                  -0.3587704300880432f,
                  0.7107815742492676f,
                  -0.5446312427520752f)
            };
            var snapshot37 = new PerformanceSnapshot
            {
                Timestamp = 18.5f,
                Location = new Vector3Ser(
                  -11.793747901916504f,
                  11.558916091918946f,
                  17.412670135498048f),
                Rotation = new QuaternionSer(
                  0.11124542355537415f,
                  0.3992907702922821f,
                  0.07584498822689057f,
                  -0.9068841934204102f)
            };
            var snapshot38 = new PerformanceSnapshot
            {
                Timestamp = 19.0f,
                Location = new Vector3Ser(
                  -11.69178295135498f,
                  10.804302215576172f,
                  17.418834686279298f),
                Rotation = new QuaternionSer(
                  0.7980367541313171f,
                  0.39698076248168948f,
                  -0.003770136274397373f,
                  -0.453353613615036f)
            };
            var snapshot39 = new PerformanceSnapshot
            {
                Timestamp = 19.5f,
                Location = new Vector3Ser(
                  -11.620627403259278f,
                  9.578481674194336f,
                  16.342866897583009f),
                Rotation = new QuaternionSer(
                  0.613122820854187f,
                  -0.6657318472862244f,
                  -0.40957534313201907f,
                  -0.11458449810743332f)
            };
            var snapshot40 = new PerformanceSnapshot
            {
                Timestamp = 20.0f,
                Location = new Vector3Ser(
                  -11.60352611541748f,
                  8.334991455078125f,
                  14.678746223449707f),
                Rotation = new QuaternionSer(
                  -0.6531608700752258f,
                  -0.5258857607841492f,
                  -0.3435121476650238f,
                  0.4228764474391937f)
            };
            var snapshot41 = new PerformanceSnapshot
            {
                Timestamp = 20.5f,
                Location = new Vector3Ser(
                  -11.599638938903809f,
                  7.226933002471924f,
                  12.662656784057618f),
                Rotation = new QuaternionSer(
                  -0.5171849131584168f,
                  0.6810108423233032f,
                  0.47079896926879885f,
                  0.21700772643089295f)
            };
            var snapshot42 = new PerformanceSnapshot
            {
                Timestamp = 21.0f,
                Location = new Vector3Ser(
                  -11.613259315490723f,
                  6.2602105140686039f,
                  10.457908630371094f),
                Rotation = new QuaternionSer(
                  0.7702630162239075f,
                  0.33762624859809878f,
                  0.265726238489151f,
                  -0.4712675213813782f)
            };
            var snapshot43 = new PerformanceSnapshot
            {
                Timestamp = 21.5f,
                Location = new Vector3Ser(
                  -11.629731178283692f,
                  5.4656877517700199f,
                  8.156707763671875f),
                Rotation = new QuaternionSer(
                  0.2790260314941406f,
                  -0.7269346714019775f,
                  -0.617722749710083f,
                  -0.1101323813199997f)
            };
            var snapshot44 = new PerformanceSnapshot
            {
                Timestamp = 22.0f,
                Location = new Vector3Ser(
                  -11.666238784790039f,
                  4.555748462677002f,
                  5.8314361572265629f),
                Rotation = new QuaternionSer(
                  -0.8615429997444153f,
                  -0.07918302714824677f,
                  -0.12114019691944123f,
                  0.48661983013153078f)
            };
            var snapshot45 = new PerformanceSnapshot
            {
                Timestamp = 22.5f,
                Location = new Vector3Ser(
                  -11.543267250061036f,
                  2.7130558490753176f,
                  3.6915271282196047f),
                Rotation = new QuaternionSer(
                  0.04184594750404358f,
                  0.8645763397216797f,
                  0.5007562041282654f,
                  0.00009440258145332336f)
            };
            var snapshot46 = new PerformanceSnapshot
            {
                Timestamp = 23.0f,
                Location = new Vector3Ser(
                  -11.608447074890137f,
                  0.5651273727416992f,
                  2.5551607608795168f),
                Rotation = new QuaternionSer(
                  0.7429344654083252f,
                  0.3790144920349121f,
                  -0.2824046015739441f,
                  -0.4739663004875183f)
            };
            var snapshot47 = new PerformanceSnapshot
            {
                Timestamp = 23.5f,
                Location = new Vector3Ser(
                  -11.689632415771485f,
                  0.5613404512405396f,
                  2.8916008472442629f),
                Rotation = new QuaternionSer(
                  0.1934620589017868f,
                  0.48202478885650637f,
                  -0.5829549431800842f,
                  -0.6248104572296143f)
            };
            var snapshot48 = new PerformanceSnapshot
            {
                Timestamp = 24.0f,
                Location = new Vector3Ser(
                  -11.168572425842286f,
                  0.6966543197631836f,
                  3.1613988876342775f),
                Rotation = new QuaternionSer(
                  0.2740355432033539f,
                  0.5538135170936585f,
                  -0.18009541928768159f,
                  -0.7653501629829407f)
            };
            var snapshot49 = new PerformanceSnapshot
            {
                Timestamp = 24.5f,
                Location = new Vector3Ser(
                  -9.731431007385254f,
                  0.661097526550293f,
                  3.2093679904937746f),
                Rotation = new QuaternionSer(
                  0.6309570074081421f,
                  0.03269602358341217f,
                  0.6501162648200989f,
                  -0.4221055805683136f)
            };
            var snapshot50 = new PerformanceSnapshot
            {
                Timestamp = 25.0f,
                Location = new Vector3Ser(
                  -8.959956169128418f,
                  0.6723086833953857f,
                  3.4066503047943117f),
                Rotation = new QuaternionSer(
                  0.4047994613647461f,
                  -0.38473081588745119f,
                  0.8250815868377686f,
                  -0.08579020947217941f)
            };
            var snapshot51 = new PerformanceSnapshot
            {
                Timestamp = 25.5f,
                Location = new Vector3Ser(
                  -9.85045051574707f,
                  0.9143787622451782f,
                  3.439561367034912f),
                Rotation = new QuaternionSer(
                  0.24378366768360139f,
                  -0.5057163834571838f,
                  0.7442774772644043f,
                  -0.36176183819770815f)
            };
            var snapshot52 = new PerformanceSnapshot
            {
                Timestamp = 26.0f,
                Location = new Vector3Ser(
                  -11.788846015930176f,
                  2.747690439224243f,
                  2.4151861667633058f),
                Rotation = new QuaternionSer(
                  0.23653444647789002f,
                  -0.6283737421035767f,
                  0.44125115871429446f,
                  -0.5953951478004456f)
            };
            var snapshot53 = new PerformanceSnapshot
            {
                Timestamp = 26.5f,
                Location = new Vector3Ser(
                  -11.817238807678223f,
                  4.5727667808532719f,
                  1.0770553350448609f),
                Rotation = new QuaternionSer(
                  0.6662666201591492f,
                  -0.14977149665355683f,
                  -0.7288445234298706f,
                  0.049426279962062839f)
            };
            var snapshot54 = new PerformanceSnapshot
            {
                Timestamp = 27.0f,
                Location = new Vector3Ser(
                  -11.8148193359375f,
                  6.009149074554443f,
                  -0.4811166524887085f),
                Rotation = new QuaternionSer(
                  -0.4946371018886566f,
                  0.5478855967521668f,
                  -0.3939289152622223f,
                  0.5477002263069153f)
            };
            var snapshot55 = new PerformanceSnapshot
            {
                Timestamp = 27.5f,
                Location = new Vector3Ser(
                  -11.74421215057373f,
                  7.117581844329834f,
                  -1.6904733180999756f),
                Rotation = new QuaternionSer(
                  -0.7892013192176819f,
                  0.239077627658844f,
                  0.5644263625144959f,
                  0.037765007466077808f)
            };
            var snapshot56 = new PerformanceSnapshot
            {
                Timestamp = 28.0f,
                Location = new Vector3Ser(
                  -11.603879928588868f,
                  8.470602035522461f,
                  -2.0783772468566896f),
                Rotation = new QuaternionSer(
                  -0.40271204710006716f,
                  -0.4696011543273926f,
                  0.49226900935173037f,
                  -0.6123471856117249f)
            };

            var snepshot1 = new PerformanceSnapshot
            {
                Timestamp = 0.5f,
                Location = new Vector3Ser(0f, 1.996075987815857f, 0f),
                Rotation = new QuaternionSer(0f, 0f, 0f, 1.0f)
            };
            var snepshot2 = new PerformanceSnapshot
            {
                Timestamp = 1.0f,
                Location = new Vector3Ser(-0.007914123125374318f, 0.7438715696334839f, 0.14086785912513734f),
                Rotation = new QuaternionSer(0f, 0f, 0f, 1.0f)
            };
            var snepshot3 = new PerformanceSnapshot
            {
                Timestamp = 1.5f,
                Location = new Vector3Ser(-0.05057601258158684f, 0.5432894825935364f, 1.0941189527511597f),
                Rotation = new QuaternionSer(0.6428659558296204f, -0.00018632641877047718f, 0.028705431148409845f, 0.7654407024383545f)
            };
            var snepshot4 = new PerformanceSnapshot
            {
                Timestamp = 2.0f,
                Location = new Vector3Ser(-0.12303921580314636f, 0.6787381768226624f, 2.8103134632110597f),
                Rotation = new QuaternionSer(0.9234148263931274f, -0.001861668424680829f, 0.039812639355659488f, -0.38172850012779238f)
            };
            var snepshot5 = new PerformanceSnapshot
            {
                Timestamp = 2.5f,
                Location = new Vector3Ser(0.05084504932165146f, 0.697434663772583f, 4.866882801055908f),
                Rotation = new QuaternionSer(-0.3198240101337433f, -0.0493151992559433f, 0.07855615764856339f, -0.9429261088371277f)
            };
            var snepshot6 = new PerformanceSnapshot
            {
                Timestamp = 3.0f,
                Location = new Vector3Ser(1.260588526725769f, 0.7084556221961975f, 6.535645484924316f),
                Rotation = new QuaternionSer(-0.8648927211761475f, 0.2255755066871643f, 0.39159733057022097f, 0.21846701204776765f)
            };
            var snepshot7 = new PerformanceSnapshot
            {
                Timestamp = 3.5f,
                Location = new Vector3Ser(3.312086582183838f, 0.7598157525062561f, 7.724052906036377f),
                Rotation = new QuaternionSer(0.2953311800956726f, 0.44268494844436648f, -0.163438081741333f, 0.8307211399078369f)
            };
            var snepshot8 = new PerformanceSnapshot
            {
                Timestamp = 4.0f,
                Location = new Vector3Ser(5.7139973640441898f, 0.7276809215545654f, 8.562849044799805f),
                Rotation = new QuaternionSer(0.6819512844085693f, -0.35008254647254946f, -0.5658206343650818f, -0.30369681119918826f)
            };
            var snepshot9 = new PerformanceSnapshot
            {
                Timestamp = 4.5f,
                Location = new Vector3Ser(7.123627185821533f,
                  0.6604511737823486f,
                  9.558085441589356f),
                Rotation = new QuaternionSer(-0.3620637357234955f,
                  -0.41333961486816409f,
                  -0.22331833839416505f,
                  -0.8051019906997681f)
            };
            var snepshot10= new PerformanceSnapshot
            {
                Timestamp = 5.0f,
                Location = new Vector3Ser(7.681317329406738f,
                  0.7406455874443054f,
                  10.76095199584961f),
                Rotation = new QuaternionSer(-0.9872421622276306f,
                  0.021687515079975129f,
                  -0.07263835519552231f,
                  -0.14002257585525514f)
            };
            var snepshot11= new PerformanceSnapshot
            {
                Timestamp = 5.5f,
                Location = new Vector3Ser(7.433403491973877f,
                  0.578610897064209f,
                  12.185981750488282f),
                Rotation = new QuaternionSer(-0.5798734426498413f,
                  -0.13515089452266694f,
                  0.08707670867443085f,
                  0.7986856698989868f)
            };
            var snepshot12= new PerformanceSnapshot
            {
                Timestamp = 6.0f,
                Location = new Vector3Ser(6.30675745010376f,
                  0.5349901914596558f,
                  13.679370880126954f),
                Rotation = new QuaternionSer(0.6059023141860962f,
                  -0.3407554030418396f,
                  0.4503704607486725f,
                  0.5602988004684448f)
            };
            var snepshot13= new PerformanceSnapshot
            {
                Timestamp = 6.5f,
                Location = new Vector3Ser(4.708486080169678f,
                  0.5530208349227905f,
                  15.23013687133789f),
                Rotation = new QuaternionSer(0.6251615285873413f,
                  0.1880246102809906f,
                  0.09863178431987763f,
                  -0.7510603070259094f)
            };
            var snepshot14= new PerformanceSnapshot
            {
                Timestamp = 7.0f,
                Location = new Vector3Ser(2.8760457038879396f,
                  0.5989409685134888f,
                  16.559497833251954f),
                Rotation = new QuaternionSer(-0.6495572924613953f,
                  0.38006624579429629f,
                  -0.5241296887397766f,
                  -0.39863914251327517f)
            };
            var snepshot15= new PerformanceSnapshot
            {
                Timestamp = 7.5f,
                Location = new Vector3Ser(
                  0.5881730318069458f,
                  0.6236072778701782f,
                  17.39064598083496f),
                Rotation = new QuaternionSer(
                  -0.5119190812110901f,
                  -0.3863008916378021f,
                  -0.017434734851121904f,
                  0.7670766115188599f)
            };
            var snepshot16= new PerformanceSnapshot
            {
                Timestamp = 8.0f,
                Location = new Vector3Ser(
                  -2.0844147205352785f,
                  0.6758556962013245f,
                  17.613418579101564f),
                Rotation = new QuaternionSer(
                  0.5954486131668091f,
                  -0.3856094181537628f,
                  0.7021976113319397f,
                  -0.060538873076438907f)
            };
            var snepshot17= new PerformanceSnapshot
            {
                Timestamp = 8.5f,
                Location = new Vector3Ser(
                  -4.329534530639648f,
                  0.6916583180427551f,
                  17.763051986694337f),
                Rotation = new QuaternionSer(
                  0.3067615032196045f,
                  0.591062068939209f,
                  -0.15780547261238099f,
                  -0.7291368246078491f)
            };
            var snepshot18= new PerformanceSnapshot
            {
                Timestamp = 9.0f,
                Location = new Vector3Ser(
                  -5.903555393218994f,
                  0.6055600643157959f,
                  17.9722900390625f),
                Rotation = new QuaternionSer(
                  -0.5814917087554932f,
                  0.537083625793457f,
                  -0.6099191308021545f,
                  -0.037512972950935367f)
            };
            var snepshot19= new PerformanceSnapshot
            {
                Timestamp = 9.5f,
                Location = new Vector3Ser(
                  -7.375308513641357f,
                  0.5350220799446106f,
                  18.435985565185548f),
                Rotation = new QuaternionSer(
                  -0.6642306447029114f,
                  -0.1892232894897461f,
                  -0.07931577414274216f,
                  0.7188193202018738f)
            };
            var snepshot20= new PerformanceSnapshot
            {
                Timestamp = 10.0f,
                Location = new Vector3Ser(
                  -9.579723358154297f,
                  0.5741841197013855f,
                  18.25315284729004f),
                Rotation = new QuaternionSer(
                  0.161786749958992f,
                  -0.6510853171348572f,
                  0.7002116441726685f,
                  0.24416516721248628f)
            };
            var snepshot21= new PerformanceSnapshot
            {
                Timestamp = 10.5f,
                Location = new Vector3Ser(
                  -11.812639236450196f,
                  1.0828194618225098f,
                  17.32432746887207f),
                Rotation = new QuaternionSer(
                  0.8298008441925049f,
                  -0.05837244912981987f,
                  0.19226792454719544f,
                  -0.5206306576728821f)
            };
            var snepshot22= new PerformanceSnapshot
            {
                Timestamp = 11.0f,
                Location = new Vector3Ser(
                  -11.771505355834961f,
                  2.5376811027526857f,
                  16.085838317871095f),
                Rotation = new QuaternionSer(
                  0.1283893883228302f,
                  0.3679145574569702f,
                  -0.9132816791534424f,
                  0.1186237558722496f)
            };
            var snepshot23= new PerformanceSnapshot
            {
                Timestamp = 11.5f,
                Location = new Vector3Ser(
                  -11.79508113861084f,
                  4.368398189544678f,
                  15.229256629943848f),
                Rotation = new QuaternionSer(
                  -0.7495535016059876f,
                  -0.023396959528326989f,
                  0.20025674998760224f,
                  0.6304914951324463f)
            };
            var snepshot24= new PerformanceSnapshot
            {
                Timestamp = 12.0f,
                Location = new Vector3Ser(
                  -11.764166831970215f,
                  6.271305561065674f,
                  15.219987869262696f),
                Rotation = new QuaternionSer(
                  0.08018004894256592f,
                  -0.6911330819129944f,
                  0.6190346479415894f,
                  -0.36428332328796389f)
            };
            var snepshot25= new PerformanceSnapshot
            {
                Timestamp = 12.5f,
                Location = new Vector3Ser(
                  -11.815001487731934f,
                  7.972695350646973f,
                  15.840571403503418f),
                Rotation = new QuaternionSer(
                  0.5039714574813843f,
                  0.3782811760902405f,
                  -0.23879535496234895f,
                  -0.7388456463813782f)
            };
            var snepshot26= new PerformanceSnapshot
            {
                Timestamp = 13.0f,
                Location = new Vector3Ser(
                  -11.748619079589844f,
                  8.865439414978028f,
                  17.067758560180665f),
                Rotation = new QuaternionSer(
                  0.1333129107952118f,
                  0.8980222940444946f,
                  -0.2388707846403122f,
                  0.3445641100406647f)
            };
            var snepshot27= new PerformanceSnapshot
            {
                Timestamp = 13.5f,
                Location = new Vector3Ser(
                  -11.814671516418457f,
                  8.937128067016602f,
                  18.460472106933595f),
                Rotation = new QuaternionSer(
                  0.05862067639827728f,
                  -0.08455207198858261f,
                  -0.07537391036748886f,
                  0.9918333888053894f)
            };
            var snepshot28= new PerformanceSnapshot
            {
                Timestamp = 14.0f,
                Location = new Vector3Ser(
                  -11.798563003540039f,
                  8.70505428314209f,
                  19.316635131835939f),
                Rotation = new QuaternionSer(
                  0.013854586519300938f,
                  -0.7784385681152344f,
                  -0.19016849994659425f,
                  0.5980613231658936f)
            };
            var snepshot29= new PerformanceSnapshot
            {
                Timestamp = 14.5f,
                Location = new Vector3Ser(
                  -11.632211685180664f,
                  8.131375312805176f,
                  19.29191017150879f),
                Rotation = new QuaternionSer(
                  -0.4073666036128998f,
                  -0.7448742985725403f,
                  -0.30332064628601076f,
                  0.43267926573753359f)
            };
            var snepshot30= new PerformanceSnapshot
            {
                Timestamp = 15.0f,
                Location = new Vector3Ser(
                  -11.758304595947266f,
                  6.961946487426758f,
                  18.097997665405275f),
                Rotation = new QuaternionSer(
                  -0.8839823007583618f,
                  0.19231542944908143f,
                  0.027874018996953965f,
                  0.4252212941646576f)
            };
            var snepshot31= new PerformanceSnapshot
            {
                Timestamp = 15.5f,
                Location = new Vector3Ser(
                  -11.749521255493164f,
                  5.644134998321533f,
                  16.401901245117189f),
                Rotation = new QuaternionSer(
                  0.14409223198890687f,
                  0.869612455368042f,
                  0.44065481424331667f,
                  -0.16980856657028199f)
            };
            var snepshot32= new PerformanceSnapshot
            {
                Timestamp = 16.0f,
                Location = new Vector3Ser(
                  -11.782419204711914f,
                  4.623794078826904f,
                  15.043045997619629f),
                Rotation = new QuaternionSer(
                  0.8873079419136047f,
                  0.1304263472557068f,
                  0.041476886719465259f,
                  -0.440401166677475f)
            };
            var snepshot33= new PerformanceSnapshot
            {
                Timestamp = 16.5f,
                Location = new Vector3Ser(
                  -11.699594497680664f,
                  4.728460311889648f,
                  13.81548023223877f),
                Rotation = new QuaternionSer(
                  0.5911105871200562f,
                  -0.2465052306652069f,
                  -0.7391800880432129f,
                  -0.20841355621814729f)
            };
            var snepshot34= new PerformanceSnapshot
            {
                Timestamp = 17.0f,
                Location = new Vector3Ser(
                  -11.601581573486329f,
                  6.006484508514404f,
                  13.380155563354493f),
                Rotation = new QuaternionSer(
                  0.28285685181617739f,
                  0.18158721923828126f,
                  -0.680652916431427f,
                  0.650945246219635f)
            };
            var snepshot35= new PerformanceSnapshot
            {
                Timestamp = 17.5f,
                Location = new Vector3Ser(
                  -11.625040054321289f,
                  8.224908828735352f,
                  13.86573600769043f),
                Rotation = new QuaternionSer(
                  -0.08883342891931534f,
                  0.12564118206501008f,
                  0.4639609456062317f,
                  0.8723893761634827f)
            };
            var snepshot36= new PerformanceSnapshot
            {
                Timestamp = 18.0f,
                Location = new Vector3Ser(
                  -11.606898307800293f,
                  10.66489028930664f,
                  15.837359428405762f),
                Rotation = new QuaternionSer(
                  -0.2635340094566345f,
                  -0.3587704300880432f,
                  0.7107815742492676f,
                  -0.5446312427520752f)
            };
            var snepshot37= new PerformanceSnapshot
            {
                Timestamp = 18.5f,
                Location = new Vector3Ser(
                  -11.793747901916504f,
                  11.558916091918946f,
                  17.412670135498048f),
                Rotation = new QuaternionSer(
                  0.11124542355537415f,
                  0.3992907702922821f,
                  0.07584498822689057f,
                  -0.9068841934204102f)
            };
            var snepshot38= new PerformanceSnapshot
            {
                Timestamp = 19.0f,
                Location = new Vector3Ser(
                  -11.69178295135498f,
                  10.804302215576172f,
                  17.418834686279298f),
                Rotation = new QuaternionSer(
                  0.7980367541313171f,
                  0.39698076248168948f,
                  -0.003770136274397373f,
                  -0.453353613615036f)
            };
            var snepshot39= new PerformanceSnapshot
            {
                Timestamp = 19.5f,
                Location = new Vector3Ser(
                  -11.620627403259278f,
                  9.578481674194336f,
                  16.342866897583009f),
                Rotation = new QuaternionSer(
                  0.613122820854187f,
                  -0.6657318472862244f,
                  -0.40957534313201907f,
                  -0.11458449810743332f)
            };
            var snepshot40= new PerformanceSnapshot
            {
                Timestamp = 20.0f,
                Location = new Vector3Ser(
                  -11.60352611541748f,
                  8.334991455078125f,
                  14.678746223449707f),
                Rotation = new QuaternionSer(
                  -0.6531608700752258f,
                  -0.5258857607841492f,
                  -0.3435121476650238f,
                  0.4228764474391937f)
            };
            var snepshot41= new PerformanceSnapshot
            {
                Timestamp = 20.5f,
                Location = new Vector3Ser(
                  -11.599638938903809f,
                  7.226933002471924f,
                  12.662656784057618f),
                Rotation = new QuaternionSer(
                  -0.5171849131584168f,
                  0.6810108423233032f,
                  0.47079896926879885f,
                  0.21700772643089295f)
            };
            var snepshot42= new PerformanceSnapshot
            {
                Timestamp = 21.0f,
                Location = new Vector3Ser(
                  -11.613259315490723f,
                  6.2602105140686039f,
                  10.457908630371094f),
                Rotation = new QuaternionSer(
                  0.7702630162239075f,
                  0.33762624859809878f,
                  0.265726238489151f,
                  -0.4712675213813782f)
            };
            var snepshot43= new PerformanceSnapshot
            {
                Timestamp = 21.5f,
                Location = new Vector3Ser(
                  -11.629731178283692f,
                  5.4656877517700199f,
                  8.156707763671875f),
                Rotation = new QuaternionSer(
                  0.2790260314941406f,
                  -0.7269346714019775f,
                  -0.617722749710083f,
                  -0.1101323813199997f)
            };
            var snepshot44= new PerformanceSnapshot
            {
                Timestamp = 22.0f,
                Location = new Vector3Ser(
                  -11.666238784790039f,
                  4.555748462677002f,
                  5.8314361572265629f),
                Rotation = new QuaternionSer(
                  -0.8615429997444153f,
                  -0.07918302714824677f,
                  -0.12114019691944123f,
                  0.48661983013153078f)
            };
            var snepshot45= new PerformanceSnapshot
            {
                Timestamp = 22.5f,
                Location = new Vector3Ser(
                  -11.543267250061036f,
                  2.7130558490753176f,
                  3.6915271282196047f),
                Rotation = new QuaternionSer(
                  0.04184594750404358f,
                  0.8645763397216797f,
                  0.5007562041282654f,
                  0.00009440258145332336f)
            };
            var snepshot46= new PerformanceSnapshot
            {
                Timestamp = 23.0f,
                Location = new Vector3Ser(
                  -11.608447074890137f,
                  0.5651273727416992f,
                  2.5551607608795168f),
                Rotation = new QuaternionSer(
                  0.7429344654083252f,
                  0.3790144920349121f,
                  -0.2824046015739441f,
                  -0.4739663004875183f)
            };
            var snepshot47= new PerformanceSnapshot
            {
                Timestamp = 23.5f,
                Location = new Vector3Ser(
                  -11.689632415771485f,
                  0.5613404512405396f,
                  2.8916008472442629f),
                Rotation = new QuaternionSer(
                  0.1934620589017868f,
                  0.48202478885650637f,
                  -0.5829549431800842f,
                  -0.6248104572296143f)
            };
            var snepshot48= new PerformanceSnapshot
            {
                Timestamp = 24.0f,
                Location = new Vector3Ser(
                  -11.168572425842286f,
                  0.6966543197631836f,
                  3.1613988876342775f),
                Rotation = new QuaternionSer(
                  0.2740355432033539f,
                  0.5538135170936585f,
                  -0.18009541928768159f,
                  -0.7653501629829407f)
            };
            var snepshot49= new PerformanceSnapshot
            {
                Timestamp = 24.5f,
                Location = new Vector3Ser(
                  -9.731431007385254f,
                  0.661097526550293f,
                  3.2093679904937746f),
                Rotation = new QuaternionSer(
                  0.6309570074081421f,
                  0.03269602358341217f,
                  0.6501162648200989f,
                  -0.4221055805683136f)
            };
            var snepshot50= new PerformanceSnapshot
            {
                Timestamp = 25.0f,
                Location = new Vector3Ser(
                  -8.959956169128418f,
                  0.6723086833953857f,
                  3.4066503047943117f),
                Rotation = new QuaternionSer(
                  0.4047994613647461f,
                  -0.38473081588745119f,
                  0.8250815868377686f,
                  -0.08579020947217941f)
            };
            var snepshot51= new PerformanceSnapshot
            {
                Timestamp = 25.5f,
                Location = new Vector3Ser(
                  -9.85045051574707f,
                  0.9143787622451782f,
                  3.439561367034912f),
                Rotation = new QuaternionSer(
                  0.24378366768360139f,
                  -0.5057163834571838f,
                  0.7442774772644043f,
                  -0.36176183819770815f)
            };
            var snepshot52= new PerformanceSnapshot
            {
                Timestamp = 26.0f,
                Location = new Vector3Ser(
                  -11.788846015930176f,
                  2.747690439224243f,
                  2.4151861667633058f),
                Rotation = new QuaternionSer(
                  0.23653444647789002f,
                  -0.6283737421035767f,
                  0.44125115871429446f,
                  -0.5953951478004456f)
            };
            var snepshot53= new PerformanceSnapshot
            {
                Timestamp = 26.5f,
                Location = new Vector3Ser(
                  -11.817238807678223f,
                  4.5727667808532719f,
                  1.0770553350448609f),
                Rotation = new QuaternionSer(
                  0.6662666201591492f,
                  -0.14977149665355683f,
                  -0.7288445234298706f,
                  0.049426279962062839f)
            };
            var snepshot54= new PerformanceSnapshot
            {
                Timestamp = 27.0f,
                Location = new Vector3Ser(
                  -11.8148193359375f,
                  6.009149074554443f,
                  -0.4811166524887085f),
                Rotation = new QuaternionSer(
                  -0.4946371018886566f,
                  0.5478855967521668f,
                  -0.3939289152622223f,
                  0.5477002263069153f)
            };
            var snepshot55= new PerformanceSnapshot
            {
                Timestamp = 27.5f,
                Location = new Vector3Ser(
                  -11.74421215057373f,
                  7.117581844329834f,
                  -1.6904733180999756f),
                Rotation = new QuaternionSer(
                  -0.7892013192176819f,
                  0.239077627658844f,
                  0.5644263625144959f,
                  0.037765007466077808f)
            };
            var snepshot56= new PerformanceSnapshot
            {
                Timestamp = 28.0f,
                Location = new Vector3Ser(
                  -11.603879928588868f,
                  8.470602035522461f,
                  -2.0783772468566896f),
                Rotation = new QuaternionSer(
                  -0.40271204710006716f,
                  -0.4696011543273926f,
                  0.49226900935173037f,
                  -0.6123471856117249f)
            };

            var performance = new PlayerPerformance
            {
                PlayerId = 1,
                PlayerName = "Rupture13",
                LevelId = 1
            };
            performance.Snapshots.Add(snapshot1);
            performance.Snapshots.Add(snapshot2);
            performance.Snapshots.Add(snapshot3);
            performance.Snapshots.Add(snapshot4);
            performance.Snapshots.Add(snapshot5);
            performance.Snapshots.Add(snapshot6);
            performance.Snapshots.Add(snapshot7);
            performance.Snapshots.Add(snapshot8);
            performance.Snapshots.Add(snapshot9);
            performance.Snapshots.Add(snapshot10);
            performance.Snapshots.Add(snapshot11);
            performance.Snapshots.Add(snapshot12);
            performance.Snapshots.Add(snapshot13);
            performance.Snapshots.Add(snapshot14);
            performance.Snapshots.Add(snapshot15);
            performance.Snapshots.Add(snapshot16);
            performance.Snapshots.Add(snapshot17);
            performance.Snapshots.Add(snapshot18);
            performance.Snapshots.Add(snapshot19);
            performance.Snapshots.Add(snapshot20);
            performance.Snapshots.Add(snapshot21);
            performance.Snapshots.Add(snapshot22);
            performance.Snapshots.Add(snapshot23);
            performance.Snapshots.Add(snapshot24);
            performance.Snapshots.Add(snapshot25);
            performance.Snapshots.Add(snapshot26);
            performance.Snapshots.Add(snapshot27);
            performance.Snapshots.Add(snapshot28);
            performance.Snapshots.Add(snapshot29);
            performance.Snapshots.Add(snapshot30);
            performance.Snapshots.Add(snapshot31);
            performance.Snapshots.Add(snapshot32);
            performance.Snapshots.Add(snapshot33);
            performance.Snapshots.Add(snapshot34);
            performance.Snapshots.Add(snapshot35);
            performance.Snapshots.Add(snapshot36);
            performance.Snapshots.Add(snapshot37);
            performance.Snapshots.Add(snapshot38);
            performance.Snapshots.Add(snapshot39);
            performance.Snapshots.Add(snapshot40);
            performance.Snapshots.Add(snapshot41);
            performance.Snapshots.Add(snapshot42);
            performance.Snapshots.Add(snapshot43);
            performance.Snapshots.Add(snapshot44);
            performance.Snapshots.Add(snapshot45);
            performance.Snapshots.Add(snapshot46);
            performance.Snapshots.Add(snapshot47);
            performance.Snapshots.Add(snapshot48);
            performance.Snapshots.Add(snapshot49);
            performance.Snapshots.Add(snapshot50);
            performance.Snapshots.Add(snapshot51);
            performance.Snapshots.Add(snapshot52);
            performance.Snapshots.Add(snapshot53);
            performance.Snapshots.Add(snapshot54);
            performance.Snapshots.Add(snapshot55);
            performance.Snapshots.Add(snapshot56);

            var performance2 = new PlayerPerformance
            {
                PlayerId = 2,
                PlayerName = "Vasharnesh",
                LevelId = 1
            };
            performance2.Snapshots.Add(snepshot1);
            performance2.Snapshots.Add(snepshot2);
            performance2.Snapshots.Add(snepshot3);
            performance2.Snapshots.Add(snepshot4);
            performance2.Snapshots.Add(snepshot5);
            performance2.Snapshots.Add(snepshot6);
            performance2.Snapshots.Add(snepshot7);
            performance2.Snapshots.Add(snepshot8);
            performance2.Snapshots.Add(snepshot9);
            performance2.Snapshots.Add(snepshot10);
            performance2.Snapshots.Add(snepshot11);
            performance2.Snapshots.Add(snepshot12);
            performance2.Snapshots.Add(snepshot13);
            performance2.Snapshots.Add(snepshot14);
            performance2.Snapshots.Add(snepshot15);
            performance2.Snapshots.Add(snepshot16);
            performance2.Snapshots.Add(snepshot17);
            performance2.Snapshots.Add(snepshot18);
            performance2.Snapshots.Add(snepshot19);
            performance2.Snapshots.Add(snepshot20);
            performance2.Snapshots.Add(snepshot21);
            performance2.Snapshots.Add(snepshot22);
            performance2.Snapshots.Add(snepshot23);
            performance2.Snapshots.Add(snepshot24);
            performance2.Snapshots.Add(snepshot25);
            performance2.Snapshots.Add(snepshot26);
            performance2.Snapshots.Add(snepshot27);
            performance2.Snapshots.Add(snepshot28);
            performance2.Snapshots.Add(snepshot29);
            performance2.Snapshots.Add(snepshot30);
            performance2.Snapshots.Add(snepshot31);
            performance2.Snapshots.Add(snepshot32);
            performance2.Snapshots.Add(snepshot33);
            performance2.Snapshots.Add(snepshot34);
            performance2.Snapshots.Add(snepshot35);
            performance2.Snapshots.Add(snepshot36);
            performance2.Snapshots.Add(snepshot37);
            performance2.Snapshots.Add(snepshot38);
            performance2.Snapshots.Add(snepshot39);
            performance2.Snapshots.Add(snepshot40);
            performance2.Snapshots.Add(snepshot41);
            performance2.Snapshots.Add(snepshot42);
            performance2.Snapshots.Add(snepshot43);
            performance2.Snapshots.Add(snepshot44);
            performance2.Snapshots.Add(snepshot45);
            performance2.Snapshots.Add(snepshot46);
            performance2.Snapshots.Add(snepshot47);
            performance2.Snapshots.Add(snepshot48);
            performance2.Snapshots.Add(snepshot49);
            performance2.Snapshots.Add(snepshot50);
            performance2.Snapshots.Add(snepshot51);
            performance2.Snapshots.Add(snepshot52);
            performance2.Snapshots.Add(snepshot53);
            performance2.Snapshots.Add(snepshot54);
            performance2.Snapshots.Add(snepshot55);
            performance2.Snapshots.Add(snepshot56);

            this.PlayerPerformances.Add(performance);
            this.PlayerPerformances.Add(performance2);

            this.SaveChanges();
        }

    }
}
