﻿// Copyright 2017 Google Inc. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Xunit;

namespace Google.Cloud.Iam.V1.Tests
{
    public class PolicyTest
    {
        [Fact]
        public void AddRoleMember_RoleExists_MemberExists()
        {
            var policy = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                    new Binding { Role = "target", Members = { "a", "b", "c" } },
                }
            };
            var expected = policy.Clone();
            Assert.False(policy.AddRoleMember("target", "b"));
            Assert.Equal(expected, policy); // No change
        }

        [Fact]
        public void AddRoleMember_RoleExists_NewMember()
        {
            var policy = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                    new Binding { Role = "target", Members = { "a", "b", "c" } },
                }
            };
            var expected = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                    new Binding { Role = "target", Members = { "a", "b", "c", "d" } },
                }
            };
            Assert.True(policy.AddRoleMember("target", "d"));
            Assert.Equal(expected, policy);
        }

        [Fact]
        public void AddRoleMember_NewRole()
        {
            var policy = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                }
            };
            var expected = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                    new Binding { Role = "target", Members = { "d" } },
                }
            };
            Assert.True(policy.AddRoleMember("target", "d"));
            Assert.Equal(expected, policy);
        }

        [Fact]
        public void RemoveRoleMember_RoleExists_MemberExists_OtherMembers()
        {
            var policy = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                    new Binding { Role = "target", Members = { "a", "b", "c" } },
                }
            };
            var expected = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                    new Binding { Role = "target", Members = { "a", "c" } },
                }
            };
            Assert.True(policy.RemoveRoleMember("target", "b"));
            Assert.Equal(expected, policy);
        }

        [Fact]
        public void RemoveRoleMember_RoleExists_MemberExists_LastMember()
        {
            var policy = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                    new Binding { Role = "target", Members = { "b" } },
                }
            };
            var expected = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                }
            };
            Assert.True(policy.RemoveRoleMember("target", "b"));
            Assert.Equal(expected, policy);
        }

        [Fact]
        public void RemoveRoleMember_RoleExists_NoMember()
        {
            var policy = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                    new Binding { Role = "target", Members = { "a", "b", "c" } },
                }
            };
            var expected = policy.Clone();
            Assert.False(policy.RemoveRoleMember("target", "d"));
            Assert.Equal(expected, policy);
        }

        [Fact]
        public void RemoveRoleMember_NoRole()
        {
            var policy = new Policy
            {
                Bindings =
                {
                    new Binding { Role = "other", Members = { "x", "y", "z" } },
                }
            };
            var expected = policy.Clone();
            Assert.False(policy.RemoveRoleMember("target", "d"));
            Assert.Equal(expected, policy);
        }
    }
}
