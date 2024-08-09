using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Meowies.Models;

namespace Meowies.ViewModels;

public class SignUpViewModel : ProfileViewModelBase
{
    public static string Message { get; set; } = "";
    
    [Required]
    [EmailAddress]
    public static string MailAddress { get; set; } = "";

    [Required]
    public static string Password { get; set; } = "";

    [Required]
    public static string Name { get; set; } = "";
    public static DateTime Birthday { get; set; } = DateTime.Today;
    private static readonly Random Rnd = new();
    public static User NewUser =>
        new()
        {
            Name = Name,
            Birthday = Birthday.ToString(CultureInfo.CurrentCulture),
            Password = Password,
            Email = MailAddress,
            ProfilePicture = Rnd.Next(1, 13)
        };

    public string Consent { get; set; }  = """
                                           I (hereinafter referred to as the “Personal Data Subject”), being legally capable, freely, voluntarily, and for my own benefit, to the satisfaction of Federal Law № 152-FZ "On Personal Data" dated July 27, 2006 (hereinafter referred to as the “Law On Personal Data”), hereby provide this Consent Form for processing personal data (hereinafter referred to as the “Consent Form”) to the Autonomous non-profit organization "Directorate of the World Youth Festival”, OGRN 1237700328194, INN 9704210995, located at the address: 121099, Moscow, Novinsky Boulevard, 3/1.

                                           The provision and confirmation of the Consent Form in question imply checking the appropriate box and clicking the “Submit” button when registering on the official website of the World Youth Festival 2024, https://fest2024.com/ (https://fest2024.ru/) (hereinafter referred to as the “Website”) owned by the Operator, i.e. submitting an online application form to participate in the World Youth Festival 2024 (hereinafter referred to as the “Festival”).

                                           I also give my consent to the processing of my personal data and their transfer to any other third parties (hereinafter referred to as the “Third Parties”) engaged by the Operator for the purpose of organizing and conducting the Festival.

                                           This Consent form applies to processing (collection, recording, systematization, accumulation, storage, clarification (update, change), extraction, use, distribution (including publishing in mass media, on the Website, third-party sites, and other Internet resources, posting information about the Festival), provision of access, depersonalization, blocking, deletion, destruction of personal data, in accordance with the legislation of the Russian Federation) of my following personal data:

                                           last name, first name, patronymic (if there is any);
                                           sex;
                                           age;
                                           date and place of birth;
                                           passport data or other identity document (including number, issuing authority, date of issue);
                                           photo;
                                           place of work/study (name of the organization, position);
                                           data of the Educational Certificate (or a document containing information about the results of the last certification of the last year of study, if the studies have not finished yet);
                                           qualification (level);
                                           data on personal and professional achievements, hobbies;
                                           contact information (phone, e-mail, social media accounts);
                                           information about professional and personal qualities;
                                           foreign language skills;
                                           clothing size, height, weight, nationality, other information requested by the Operator, as well as provided to the Operator by me during the organization and holding of the Festival (hereinafter referred to as the “Personal Data”).
                                           I give the consent because of my participation/potential participation in the Festival for the following purposes (including, but not limited to):

                                           identification of the Personal Data Subject;
                                           interaction between the Operator and Third parties with the Personal Data Subject, including sending requests, notifications,
                                           information related to the activities of the Operator and/or Third parties, as well as processing requests, applications, and other correspondence from the data subject;

                                           sending information materials about the Festival to the Personal Data Subject;
                                           preparation of documents required for the purpose of the Personal Data Subject's participation in the Festival;
                                           ensuring compliance with regulatory and non-regulatory legal acts of the Russian Federation and the subjects of Russian Federation, decisions, instructions, and requests of state authorities and/or local self-government bodies and/or persons acting on behalf of such authorities;
                                           ensuring the necessary level of security, including verification of information about the Personal Data Subject by the competent authorities, compliance with the access regime and control of its compliance, including registration of a document for access to the Festival venue, video surveillance and video recording on the territory and in the buildings/premises/venues of the Festival;
                                           participation of the Personal Data Subject in events, projects, activities within the framework of the preparation and conduct of the Festival;
                                           conducting statistics and analysis by territorial, age, and other criteria of participation in the Festival;
                                           other purposes related to the specifics of the Festival and requirements for its conduct.
                                           The Personal Data storage is provided by the Operator in a form that allows their processing no longer than required to achieve these purposes.

                                           The Operator takes the necessary, sufficient, organizational and technical measures to protect the personal information of the Personal Data Subject from illegal or accidental access, destruction, modification, blocking, copying, distribution, as well as from other illegal actions of Third Parties.

                                           The condition for the termination of the personal data processing can be the purpose achievement of personal data processing, the expiration of personal data processing, the withdrawal of Consent by the Personal Data Subject, as well as in case of the identification of illegal personal data processing.

                                           The personal data of the Personal Data Subject must be dispozed or depersonalized upon achieving of the processing purposes or in case of loss of necessity to achieve these purposes.

                                           The Personal Data Subject is familiar with and agrees that according to the contact details provided (e-mail address, phone number), the Operator and/or Third Parties have the right to send messages and SMS notifications/messages, including informational and newsletters, surveys, test materials, invitations to Operator events and/or Third Parties, as well as for the purpose of confirming the identity of the Personal Data Subject.

                                           The Personal Data Subject has the right to:

                                           – provide complete information about his/her Personal Data processed by the Operator and Third parties;

                                           – access his/her Personal Data, including the right to receive a copy of any record containing the personal data, except for the cases stipulated by the legislation of the Russian Federation.

                                           – clarification, blocking or destruction of Personal Data in case of any incomplete, outdated, inaccurate, illegally received or not necessary for the stated purpose of processing;

                                           – withdrawal of consent to the processing of Personal Data;

                                           – taking actions to protect his/her rights provided by the legislation of the Russian Federation.

                                           – appeal to the authorized body or in court for the protection of the rights of illegal actions during processing his Personal Data;

                                           – exercise other rights provided by the legislation of the Russian Federation.

                                           The Consent Form may be withdrawn by the Personal Data Subject or by his/her duly authorized representative at least 30 (thirty) days before the expected date of termination of the processing of Personal Data by the Operator by sending a written application to the Operator at the email address info@fest2024.com

                                           If the Personal Data Subject or his/her duly authorized representative withdraw the Consent Form, the Operator has the right to continue processing Personal Data without the the Subject’s permission if there are grounds specified in paragraphs 2-11 of part 1 of article 6, part 2 of article 10 of the Law On Personal Data.
                                           """;
}