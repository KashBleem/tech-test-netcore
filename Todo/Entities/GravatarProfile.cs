using System;
using System.Collections.Generic;

public class GravatarProfile
{
    public string Hash { get; set; }
    public string display_name { get; set; }
    public string avatar_url { get; set; }
    public string avatar_alt_text { get; set; }
    public string location { get; set; }
    public string description { get; set; }
    public string job_title { get; set; }
    public string company { get; set; }
    public List<object> verified_accounts { get; set; }
    public string pronunciation { get; set; }
    public string pronouns { get; set; }
    public string profile_url { get; set; }
    public Payments payments { get; set; }
    public ContactInfo contact_info { get; set; }
    public List<object> links { get; set; }
    public List<object> gallery { get; set; }
    public int number_verified_accounts { get; set; }
    public DateTime last_profile_edit { get; set; }
    public DateTime registration_date { get; set; }
    public List<object> interests { get; set; }
}

public class Payments
{
    public List<object> links { get; set; }
    public List<object> crypto_wallets { get; set; }
}

public class ContactInfo
{
    public string home_phone { get; set; }
    public string work_phone { get; set; }
    public string cell_phone { get; set; }
    public string email { get; set; }
    public string contact_form { get; set; }
    public string calendar { get; set; }
}
